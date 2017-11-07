using System;
using Android.App;
using Android.Widget;
using Android.OS;
using static Android.Views.View;
using System.Net.Http;

namespace ClientCalcDroid.Droid
{
    [Activity (Label = "ClientCalcDroid.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        System.Net.Http.HttpClient httpC;

        protected override void OnCreate (Bundle bundle)
		{
            httpC = new System.Net.Http.HttpClient();
			base.OnCreate (bundle);
            SetContentView(Resource.Layout.Main);
            Button[] buttonNum = { FindViewById<Button>(Resource.Id.button7), FindViewById<Button>(Resource.Id.button8), FindViewById<Button>(Resource.Id.button9),
            FindViewById<Button>(Resource.Id.button4), FindViewById<Button>(Resource.Id.button5), FindViewById<Button>(Resource.Id.button6), FindViewById<Button>(Resource.Id.button1),
            FindViewById<Button>(Resource.Id.button2), FindViewById<Button>(Resource.Id.button3), FindViewById<Button>(Resource.Id.buttondot), FindViewById<Button>(Resource.Id.button0)};
            for(int i = 0; i < buttonNum.Length; i++)
            {
                buttonNum[i].Click += new EventHandler(ClicBtn);
            }
            Button[] buttonOpr = { FindViewById<Button>(Resource.Id.add), FindViewById<Button>(Resource.Id.riz), FindViewById<Button>(Resource.Id.mult), FindViewById<Button>(Resource.Id.div) };
            for (int i = 0; i < buttonOpr.Length; i++)
            {
                buttonOpr[i].Click += new EventHandler(Opr_Click);
            }
            Button buttonC = FindViewById<Button>(Resource.Id.buttonC);
            buttonC.Click += new EventHandler(AllClear_Click);
            Button buttonClearOne = FindViewById<Button>(Resource.Id.cler);
            buttonClearOne.Click += new EventHandler(button18_Click);
            Button buttonRes = FindViewById<Button>(Resource.Id.res);
            buttonRes.Click += new EventHandler(Res_Click);
        }

        
        string fNum;
        string sNum;
        string op;
        string url = "http://localhost:8080/";
        private void ClicBtn(object s, EventArgs e)
        {
            FindViewById<TextView>(Resource.Id.textView1).Text += ((Button)s).Text;
        }
        private void Opr_Click(object sender, EventArgs e)
        {
            if (FindViewById<TextView>(Resource.Id.textView1).Text != "")
            {
                fNum = FindViewById<TextView>(Resource.Id.textView1).Text;
                op = ((Button)sender).Text;
                if (op == "+") { op = "plus"; }
                FindViewById<TextView>(Resource.Id.textView1).Text = "";
            }
        }
        private async void Res_Click(object sender, EventArgs e)
        {
            if (FindViewById<TextView>(Resource.Id.textView1).Text != "")
            {
                sNum = FindViewById<TextView>(Resource.Id.textView1).Text;
                try
                {
                    FindViewById<TextView>(Resource.Id.textView1).Text = await httpC.GetStringAsync($"{url}?num1={fNum}&num2={sNum}&opr={op}");
                }
                catch (HttpRequestException)
                {
                    FindViewById<TextView>(Resource.Id.textView1).Text = "server not respond";
                }
            }
        }

        private void AllClear_Click(object sender, EventArgs e)
        {
            fNum = sNum = op = null;
            FindViewById<TextView>(Resource.Id.textView1).Text = "";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (FindViewById<TextView>(Resource.Id.textView1).Text != "")
            {
                FindViewById<TextView>(Resource.Id.textView1).Text = FindViewById<TextView>(Resource.Id.textView1).Text.Remove(FindViewById<TextView>(Resource.Id.textView1).Text.Length - 1, 1);
            }
        }
    }
}


