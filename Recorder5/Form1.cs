using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio.Wave;
using System.IO;
using Google.Cloud.Translation.V2;
using Google.Cloud.Speech.V1;
using Google.Cloud.Storage.V1;

using Newtonsoft.Json;

using System.Net.Http;
using System.Net;

using MySql.Data.MySqlClient;


namespace Recorder5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            // setup WaveIn Devices
            //souce をクラス化：WaveInCapabilities
            List<WaveInCapabilities> sources = new List<WaveInCapabilities>();

            //sources に 利用できるデバイスをDeviceCount 数分 追加する。
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                sources.Add(WaveIn.GetCapabilities(i));
            }

            listview_sources.Items.Clear();

            //listview_sourc にitem を表示する。

            foreach (var source in sources)
            {
                ListViewItem item = new ListViewItem(source.ProductName);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, source.Channels.ToString()));

                listview_sources.Items.Add(item);
            }
        }


        #region Member

        private NAudio.Wave.WaveIn sourceStream = null;
        private NAudio.Wave.DirectSoundOut waveOut = null;
        private NAudio.Wave.WaveFileWriter waveWriter = null;




        #endregion

        private void button_Start_Click(object sender, EventArgs e)
        {

            //マイク元を指定していない場合。
            if (listview_sources.SelectedItems.Count == 0) return;

            //オーディオチェーン:WaveIn(rec)  ⇒ Callback() ⇒ waveWriter

            //録音先のwavファイル
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Wave File (*.wav)|*.wav;";
            if (save.ShowDialog() != DialogResult.OK) return;

            //選択した録音デバイス番号
            int deviceNumber = listview_sources.SelectedItems[0].Index;

            //waveIn selet Recording Deivce

            sourceStream = new WaveIn();　　//sourceStreamは、78で定義
            sourceStream.DeviceNumber = deviceNumber;
            //  sourceStream.WaveFormat = new WaveFormat(16000, WaveIn.GetCapabilities(deviceNumber).Channels);
            sourceStream.WaveFormat = new WaveFormat(16000, 1);

            //録音のコールバックkな数 k??
            sourceStream.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStream_DataAvailable);

            //wave 出力
            waveWriter = new WaveFileWriter(save.FileName, sourceStream.WaveFormat);


            //Label
            this.Label_Status.Text = "録音中" + "\r\n" + "開始時間:" + DateTime.Now; ;

            //録音開始
            sourceStream.StartRecording();

        }

        private void sourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            waveOut = null;

            sourceStream?.StopRecording();
            sourceStream?.Dispose();
            sourceStream = null;

            waveWriter?.Dispose();
            waveWriter = null;

            //Label
            this.Label_Status.Text = "待機中";



        }

        private void btn_Upload_Click(object sender, EventArgs e)　　　　//GoogleへWAVファイルのアップロード
        {

            //https://qiita.com/shiki_hskw/items/b8f365d6b8075bf4c29b
            //https://leadtools.grapecity.com/topics/news-20170726
            //https://cloud.google.com/storage/docs/object-basics?hl=ja#storage-download-object-python

            string bucketName = "kh1009";
            string localPath = @"C:\Temp\";
            string fileName = "translate.wav";

            //label
            this.Label_Status.Text = "Google Cloud ストレージ へアップ中" + "\r\n" + "開始時間:" + DateTime.Now;

            try
            {
                var storage = StorageClient.Create();
                using (var f = File.OpenRead(localPath + "/" + fileName))
                {
                    //objectName = objectName ?? Path.GetFileName(localPath);
                    storage.UploadObject(bucketName, fileName, null, f);
                    MessageBox.Show($"Uploaded {fileName}.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                //Console.WriteLine(exc.Message);
                return;
            }

            //label
            this.Label_Status.Text = "待機中";
        }

        static object AsyncRecognize(string storageUri)
        {
            var speech = SpeechClient.Create();
            DateTime Start_time = DateTime.Now;

            var longOperation = speech.LongRunningRecognize(
                new RecognitionConfig()
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                    SampleRateHertz = 16000,
                    LanguageCode = "ja-JP",
                },
                RecognitionAudio.FromStorageUri(storageUri));

            longOperation = longOperation.PollUntilCompleted();
            var response = longOperation.Result;

            foreach (var result in response.Results)
            {
                foreach (var alternatime in result.Alternatives)
                {
                    File.AppendAllText(@"C:\temp\Japanese_txt.txt", alternatime.Transcript + Environment.NewLine, Encoding.GetEncoding("utf-16"));
                    //Pythonは、Unicode(utf-16)でないと処理できないので、エンコード指定
                }
            }

            DateTime End_time = DateTime.Now;
            MessageBox.Show("利用時間: " + Convert.ToString(End_time - Start_time));
            return 0;
        }
        private void btn_toText_Click(object sender, EventArgs e)
        {
            //The path should be gs://<bucket_name>/<file_path_inside_bucket>.  
            string storageUri = "gs://kh1009/translate.wav";

            //Console.WriteLine(storageUri);
            //File_Name.Text = "storage URI: " + storageUri;

            //System.Threading.Thread.Sleep(50);

            //label
            this.Label_Status.Text = "テキスト変換中" + "\r\n" + "開始時間:" + DateTime.Now;


            AsyncRecognize(storageUri);

            //label
            this.Label_Status.Text = "待機中";
            

        }

        private void btn_translat_Click(object sender, EventArgs e)  //日本語 ⇒ 英語 テキスト化
        {
            //string s;
            string line;
            string text = "";
            string fin_text = "";
            string targetLanguageCode = "";

            FileStream fin;
            DateTime Start_time = DateTime.Now;

            //label

            this.Label_Status.Text = "英語変換中" + "\r\n" + "開始時間:" + DateTime.Now;


            //日本語ファイルを指定する
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fin_text = ofd.FileName;
            }

            fin = new FileStream(fin_text, FileMode.Open);
            StreamReader fstr_in = new StreamReader(fin);


            //日本語DBへのアップロード用のtext_id
            //string s2 = DateTime.Now.ToString("yyMMddHH"); 
            string s2 = DateTime.Now.ToString("yyMMddHHMM");
                        
           long text_id = long.Parse(s2) * 100 + 1; // 初期値は、yymmddHHMM001



            while ((line = fstr_in.ReadLine()) != null)
            {

                Japanese_Upload(line,text_id);  //日本語DBへアップロード
                text_id++;

                text = text + line;
                text = text + "\r\n";  //改行コード追加　　https://www.ipentec.com/document/csharp-text-load-from-file-line                
            }            
            fstr_in.Close();


            //ターゲット言語を指定
            if (this.List_Language.SelectedItems.Count == 0) return;


             targetLanguageCode = List_Language.SelectedItem.ToString();

            switch (this.List_Language.SelectedItem)
            {
                case  "English":
                    targetLanguageCode = "en";
                    break;

                case "China":
                    targetLanguageCode = "chaina";
                    break;

                case "German":
                    targetLanguageCode = "German";
                    break;

                case "France":
                    targetLanguageCode = "fr";
                    break;
            }


            string sourceLanguageCode = "ja";

            TranslationClient client = TranslationClient.Create();
            var response = client.TranslateText(text, targetLanguageCode, sourceLanguageCode);

            File.WriteAllText(@"C:\temp\English.txt", Convert.ToString(response.TranslatedText), Encoding.GetEncoding("utf-16"));
            //File.AppendAllText(@"C:\temp\English.txt", Convert.ToString(response.TranslatedText), Encoding.GetEncoding("utf-16"));
            //http://www.atmarkit.co.jp/fdotnet/dotnettips/680filewriteall/filewriteall.html

            // Status 用
            DateTime End_time = DateTime.Now;
            MessageBox.Show("利用時間: " + Convert.ToString(End_time - Start_time));

            this.Label_Status.Text = "待機中" + "\r\n" + @" C:\temp\English.txtで保存";
            //Console.WriteLine(resonse.TranslatedText);
            //Console.ReadKey();

        }

        static void Japanese_Upload(string line,long text_id)   //日本語DBへアップロード
        {
            //mySQL on Azureにデータ蓄積
            //MySQLとの接続情報
            string server = "khmysql0107.mysql.database.azure.com";
            string user = "k-honda@khmysql0107.mysql.database.azure.com";
            string pass = "passworD@0";
            string database = "khondadb";
            string charset = "sjis";
            
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}; Charset={4};", server, database, user, pass, charset);
            //https://ameblo.jp/ringotabeta/entry-10608741673.html


            //line = Encoding.GetEncoding("utf-8");
            try
            {
                MySqlConnection MyCN = new MySqlConnection(connectionString);
                MyCN.Open();

                //SQL文
                string strSQL = @"insert into Japanese_tbl(Japanese_txt,input_date,text_id) values (@Japanese_txt,@input_date,@text_id)";
                MySqlCommand MyCmd = new MySqlCommand(strSQL, MyCN);

                MyCmd.Parameters.Add(new MySqlParameter("@Japanese_txt", line));
                MyCmd.Parameters.Add(new MySqlParameter("@input_date", DateTime.Now.ToString("yyyy-MM-dd")));
                MyCmd.Parameters.Add(new MySqlParameter("@text_id", text_id));

                MyCmd.ExecuteNonQuery();

                MyCN.Close();
            }
            catch (MySqlException MEx)
            {
                MessageBox.Show("エラー");
            }
        }


        private void listview_sources_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_keyPhase_Click(object sender, EventArgs e)　//英語テキストからkeyphaseを抽出
        {


            string PostJson = "";
            string ResponseJson = "";
            string English_text = "";

            //DBへのアップロード用のtext_id
            string s2 = DateTime.Now.ToString("yyMMddHH");
            int text_id = int.Parse(s2) * 100 + 1; // 初期値は、yyMMddHH001


            //JsonFile作成           
            MakeJSON(ref PostJson,English_text,ref text_id);   //参照渡し：https://dobon.net/vb/dotnet/beginner/byvalbyref.html                                               

            this.Label_Status.Text = "Post中";
            MakeRequest(PostJson,ref ResponseJson);

             this.Label_Status.Text = "MySQLにアップロード中";
            UploadMySQL(ResponseJson,text_id);  //mySQLにアップロード 
            
              this.Label_Status.Text = "完了";

        }

        static void MakeJSON(ref string PostJson,string English_text,ref int text_id)
        {

            int counter = 1;
            string line;

            //string English_text="";

            //英語テキストを指定する
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                English_text = ofd.FileName;

            }

            //textファイルの読み込み
            //StreamReader sr = new StreamReader(@"D:\SelfStudy\PDF1\UFR.txt", Encoding.GetEncoding("shift_jis"));
            StreamReader sr = new StreamReader(English_text, Encoding.GetEncoding("shift_jis"));
            

            //ルートオブジェクトをインスタンス化
            RootObject RO = new RootObject()
            {
                documents = new List<documents>()
            };


            // テキスト内容をJSONファイル化

            //documents userdata = new documents();

            while ((line = sr.ReadLine()) != null)
            {
                documents userdata = new documents();
                userdata.language = "en";
                userdata.id = Convert.ToString(counter);  //https://www.sejuku.net/blog/44977
                userdata.text = line;
                  

                  English_Upload(line,text_id);  //英語テキストをMySQLに保存
                  text_id++;

                counter++;
                RO.documents.Add(userdata);
            }

            PostJson = JsonConvert.SerializeObject(RO);
            sr.Close();
        }

        static void English_Upload(string line,int text_id)
        {
            //mySQL on Azureにデータ蓄積
            //MySQLとの接続情報
            string server = "khmysql0107.mysql.database.azure.com";
            string user = "k-honda@khmysql0107.mysql.database.azure.com";
            string pass = "passworD@0";
            string database = "khondadb";

            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);


            try
            {
                MySqlConnection MyCN = new MySqlConnection(connectionString);
                MyCN.Open();
               
                        //SQL文
                        string strSQL = @"insert into English_tbl(English_txt,input_date,text_id) values (@English_txt,@input_date,@text_id)";
                        MySqlCommand MyCmd = new MySqlCommand(strSQL, MyCN);
                        
                        MyCmd.Parameters.Add(new MySqlParameter("@English_txt", line));
                        MyCmd.Parameters.Add(new MySqlParameter("@input_date", DateTime.Now.ToString("yyyy-MM-dd")));
                        MyCmd.Parameters.Add(new MySqlParameter("@text_id", text_id));

                MyCmd.ExecuteNonQuery();

                MyCN.Close();
            }
            catch (MySqlException MEx)
            {
                MessageBox.Show("エラー");
            }
        }


        //Make Request 

        static void MakeRequest(string PostJson,ref string responseJson)
        {
            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases";


            HttpWebRequest Wreq = (HttpWebRequest)HttpWebRequest.Create(uri);
            Wreq.Headers.Add("Ocp-Apim-Subscription-Key:7cfd2ff5b5f64ea08edd6f5b37793a06");
            Wreq.Method = "POST";

            //文字コードをUTF-8に変換
            byte[] BODY = Encoding.UTF8.GetBytes(PostJson);
            Wreq.ContentLength = BODY.Length;  //BODYの長さを設定

            //送信するBODYをWebReqestにセットする

            Stream Wstream = Wreq.GetRequestStream();
            Wstream.Write(BODY, 0, BODY.Length);

            //サーバのリクエスト送信
            HttpWebResponse Wres = (HttpWebResponse)Wreq.GetResponse();



            //受け取ったResponse：(バイナリ）から文字列として読み出す。System.IO を利用する

            Stream WresStream = Wres.GetResponseStream();
            StreamReader WSReader = new StreamReader(WresStream);
            responseJson = WSReader.ReadToEnd();

            //string result = WSReader.ReadToEnd();
            


            /*****

           //デシリアライズでデータを取り出そう
           Re_RootObject re_RO = JsonConvert.DeserializeObject<Re_RootObject>(result);


           //mySQL on Azureにデータ蓄積



           //IDとKeyPhaseを出力
           foreach (Re_Document re_Do in re_RO.documents)
           {
               Console.WriteLine(re_Do.id);
               foreach (string keys in re_Do.keyPhrases)
               {
                   Console.WriteLine(" {0} ", keys);
               }


           }

           Console.Read();
           ******/
        }

        //Make Request 

        static void UploadMySQL(string ResponseJson,int text_id)
        {           

            //mySQL on Azureにデータ蓄積
            //MySQLとの接続情報
            string server = "khmysql0107.mysql.database.azure.com";
            string user = "k-honda@khmysql0107.mysql.database.azure.com";
            string pass = "passworD@0";
            string database = "khondadb";

            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);


            try
            {
                MySqlConnection MyCN = new MySqlConnection(connectionString);
                MyCN.Open();
                //MessageBox.Show("MySQL on Azureに接続できた");
                               
                //デシリアライズでデータを取り出そう
                Re_RootObject re_RO = JsonConvert.DeserializeObject<Re_RootObject>(ResponseJson);

                //IDとKeyPhaseを出力
                //int count_i = 1; //Text_id

                //DBへのアップロード用のtext_id (再初期化）
                string s2 = DateTime.Now.ToString("yyMMddHH");
                text_id = int.Parse(s2) * 100 + 1; // 初期値は、yyMMddHH001


                int count_j = 1;  //keyphase id
                foreach (Re_Document re_Do in re_RO.documents)
                {                                  
                    foreach (string keys in re_Do.keyPhrases)
                    {
                        
                        //SQL文
                        string strSQL = @"insert into keyphasetbl(text_id,KeyPhase_id,Sentence,KeyPhase,Input_date) values (@text_id,@KeyPhase_id,@Sentense,@keyphase,@input_date)";
                        MySqlCommand MyCmd = new MySqlCommand(strSQL, MyCN);
                        //MyCmd.Parameters.Add(new MySqlParameter("@text_id", count_i));
                        MyCmd.Parameters.Add(new MySqlParameter("@text_id", text_id));
                        MyCmd.Parameters.Add(new MySqlParameter("@KeyPhase_id", count_j));

                        MyCmd.Parameters.Add(new MySqlParameter("@Sentense", "sentence"));
                        MyCmd.Parameters.Add(new MySqlParameter("@keyphase", keys));
                        MyCmd.Parameters.Add(new MySqlParameter("@input_date", DateTime.Now.ToString("yyyy-MM-dd")));

                        MyCmd.ExecuteNonQuery();

                        count_j = count_j+1;
                    }
                    //count_i = count_i+1;
                    text_id++;
                }
            }
            catch (MySqlException MEx)
            {
                MessageBox.Show("エラー");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }




    //JSON用
    public class documents//dcoumentsの雛形、構造定義
    {
        public string language { get; set; }
        public string id { get; set; }
        public string text { get; set; }

    }

    public class RootObject //ルートオブジェクト
    {
        public List<documents> documents { get; set; }

    }




    public class Re_Document
    {
        public string id { get; set; }
        public List<string> keyPhrases { get; set; }
    }

    public class Re_RootObject
    {
        public List<Re_Document> documents { get; set; }
        public List<object> errors { get; set; }
    }
               
}

