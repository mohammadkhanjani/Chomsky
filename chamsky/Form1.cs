using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chamsky
{
    /// <summary>
    //برنامه چامسکی و تعیین حروف نقاط شکست درخت اشتقاق
    //Writed By Mohammad Khanjani
    /// </summary>
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
             

        }
        //تعریف آرایه های کمکی
        public int n = 0, i = 0;
        public string[] A = new string[100];//آرایه مربوط به ذخیره قوانین
        public char[] B = new char[15];//آرایه مربوط به ذخیره حروف کمکی
        public string[] F = new string[100];//آرایه کمکی مربوط به ذخیره قوانین
        public int[] C = new int[100];//آرایه مربوط به ذخیره جداکننده ها
        public char[,] D = new char[16,2];
        public int om = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            A[i] = textBox1.Text;
            F[i] = A[i];
            i++;
            textBox1.Clear();
            
        }
       
        //*********************** jadval 1************برای درست کردن حالات مختلف حذف حروف
        public int[,] jl; 
        public void jadval(int n)
        {
            int k = 1,ii=0,jj=n-1; bool bl = true;
            jl = new int[(int) Math.Pow(2, n),n];
          for(int i=n;i>0;i--)
          {
              ii = 0;
              for (int j = 0; j < Math.Pow(2, n) / k; j++)
              {
                  switch (bl)
                  {
                      case (true):
                          int f = k;
                          while (f != 0)
                          {
                              if (ii >= Math.Pow(2, n)) { f--; break; }
                              jl[ii++, jj] = 0;
                              f--;
                          } bl = false;
                          break;

                      case (false):
                          f = k;
                          while (f != 0)
                          {
                              if (ii >= Math.Pow(2, n)) { f--; break; }
                              jl[ii++, jj] = 1;
                              f--;
                          } bl = true;
                          break;
                  }
              }
              jj--;
              k=k*2;    
            }
          
        }
       

        //*****************************************end


        //*********************yaftan makan**یافتن مکان حرف حذفی و ذخیره آنها در آرایه 
        public struct ee
        {
           public  char ch;
           public int chi;
        }
        
        
        public void yaftan(string s,char ch)
        {
            ee[] se = new ee[50];int z=0;
            for(int yy=0;yy<s.Length;yy++)
            {
                if(s[yy]==ch)
                {
                se[z].ch=ch;
                se[z].chi=yy;
                z++;
                }
            }
            if(z!=0)    hazv(z,se,s);
        }
        ////*****************************************end

//********tabe hazv*****تابع حذف است که فراخانی می شود و با استفاده از مشخص شدن جاهای حروف حذفی و جدول حذف می کند

        public void hazv(int z,ee[] kk,string s)
        {
            int andaze = s.Length;
            string st;
            A[om] = A[om].Substring(0, A[om].Length - 1);
            jadval(z);
            int[] ctx=new int[z];
            for (int i5 = 0; i5 < Math.Pow(2, z)-1; i5++)
            {
                st =s;
                int k1 = 0;
                for (int r = 0; r < z; r++)
                    ctx[r] = kk[r].chi;
                while (k1 < z)//شکل های مختلف حذف حروف با استفاده از جدول
                {

                    if (jl[i5, k1] == 0)
                    {

                        st=st.Remove(ctx[k1],1);
                        for (int r = k1 + 1; r < z; r++)//چون بعد از حذف یکی از مکان های حروف کم می شود و رشته کوچکتر می شود یکی همه ی مکان هارا شیفت به چپ می دهیم
                            ctx[r] -= 1;
                    }
                    k1++;//خط به خط جدول پیمایش میشود
                } 
                if (A[om][A[om].Length - 1] == '|') A[om] += st;
                else
                {
                    A[om] += "|" + st;//اضافه شدن به رشته اصلی(قانون

                } 
            } A[om] +=";";
               
        }


        //********************************************قانون اول

        private void button3_Click(object sender, EventArgs e)
        {
           
            string ss = null;
            bool bt;
            
            char[] xc = new char[50];int count=0;
            char [] save=new char[50];
               int counter=0;
                for (int q = 0; q < i; q++)
                    if (A[q][A[q].Length - 2] == '$')
                    {
                        A[q] = A[q].Substring(0, A[q].Length - 3) + ';';
                        save[counter++]=A[q][0];//سمت چپ قوانینی که تهی دارند ذخیره میشود
                    }

                        for (int kk = 0; kk < i; kk++)
                        {
                                    string[] ds = A[kk].Split('=', '|', ';');//جدا میکند قسمت های مختلف یک قانون را
                                    for (int jt = 1; jt < ds.Length-1; jt++)
                                    {
                                        om = kk;
                                        for(int st=0;st<counter;st++)
                                            yaftan(ds[jt],save[st]);
                                        om = 0;                                    
                                    }
                          }
                          
            //print first rool
            listBox1.Items.Add("\n\n****hazve $****\n");
            for (int q = 0; q < i; q++)
            { listBox1.Items.Add(A[q] + "\n"); }
        }
        //*****************************************end*****پایان قانون اول******
        
        
        //******************************************مقدار اولیه قبل از اعمال تغییرات بخاطر استفاده از قوانین
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("\n\n****First Data****\n");
            for (int q = 0; q < i; q++)
            { listBox1.Items.Add(F[q] + "\n"); }
        }
        //************************************************پایان اولیه

        //*************************************************قانون دوم
        private void button2_Click_1(object sender, EventArgs e)
        {
            int t = 0; char L='F'; Boolean mm=false;
            for (int q = 0; q < i; q++)
            {
                
                string h = A[q]; ;
                
                    for (int o = 2; o <h.Length-1 ; o++)
                    {
                        mm = false;
                        if (h[o] == '|' ) 
                            continue;
                        
                        if ((int)h[o] >= 97 && (int)h[o] <= 122)
                        {   //اگر حرف بزرگ بود
                            if ((h[o - 1] == '=' || h[o - 1] == '|') && (h[o + 1] == ';' || h[o + 1] == '|')) continue;
                            h = A[q];
                            char k = 'F';
                            int s = (int)k;
                            for (int ff = 0; ff <= 9; ff++)//پر کردن آرایه کمکی
                            {
                                B[ff] = (char)s;
                                s++;
                            }
                            k = '0';
                            s = (int)k;
                            for (int ff = 10; ff <=14; ff++)
                            {
                                B[ff] = (char)s;
                                s++;
                            }
                            for (int dd = 0; dd <= t; dd++)
                            {

                                if (h[o] == D[dd, 1])
                                {
                                L = D[dd, 0];
                                mm = true;
                                break;
                                }
                            }
                            if (mm == false)
                            {
                                L = B[n];
                                n++;
                                D[t, 0] = L;
                                D[t, 1] = h[o];
                                t++;
                                A[i++] = L.ToString() + '=' + h[o];
                            }
                            A[q]=h.Substring(0,o)+L.ToString()+h.Substring(o+1,h.Length-o-1);
                                
                        }
                    }
            } //print second rool
            listBox1.Items.Add("\n****hazve payanehaye gheyre tanha****\n");
            for (int q = 0; q < i; q++)
            { listBox1.Items.Add(A[q] + "\n"); }
                                
        }
        //*****************************************end******پایان قانون دوم
        private void button5_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < i; a++)
            {
                for (int q = 0; q < i; q++)
                {
                    Array.Clear(C, 0, 100);
                    int jj = 2, t = 0, r = 1, x = 0, zz = 0; int xx = A[q].Length; C[0] = 1;
                    while (jj < xx)//جداکردن قسمت های مختلف یک قانون و ذخیره آن برای اولین بار
                    {
                        if (A[q][jj] == '|' || A[q][jj] == ';')
                        { C[r++] = jj; x++; }
                        jj++;
                    }
                    while (zz < r)
                    {
                        if (t > 0)//جداکردن قسمت های مختلف یک قانون و ذخیره آن برای بارهای بعدی بجز اولین بار
                        {
                            Array.Clear(C, 0, 100);
                            jj = 2; r = 1; zz = 0; xx = A[q].Length; C[0] = 1;
                            while (jj < xx)
                            {
                                if (A[q][jj] == '|' || A[q][jj] == ';')
                                    C[r++] = jj;
                                jj++;
                            }
                        }
                        if (x == 0) break;//اگر خالی بود
                        //ssssssssssssssssssssssssssssssssssssss t is clear C[] ,,por kardan C[]
                        if (C[zz + 1] - C[zz] - 1 == 1)//اگر اندازه یک قسمت از قانون 1 بود یعنی متغیر تنها
                        {
                            t++;
                            string ss = A[q];
                            if (A[q][C[zz]] == '=' && A[q][C[zz] + 1] < 97)
                            {
                                A[q] = A[q].Substring(0, 2) + A[q].Substring(4, A[q].Length - C[zz + 1] - 1);
                                if (A[q].Length>2)//اگر متغیر مورد نظر بعد از مساوی بود
                                    if (A[q][C[zz] + 1] == '|') A[q] = A[q].Substring(0, 2) + A[q].Substring(3, A[q].Length - C[zz + 1] - 1);//اگر بعد از حذف این متغیر حرف دیگری وجود نداشت
                            }
                            else
                            {
                                if (A[q][C[zz] + 1] < 97)
                                    if (A[q][C[zz + 1]] == '|')
                                        //اگر بین دو | بود
                                        A[q] = A[q].Substring(0, C[zz]) + A[q].Substring(C[zz + 1] + 1, A[q].Length - C[zz + 1] - 1);
                                    else//اگر بین | و ; بود
                                        A[q] = A[q].Substring(0, C[zz]) + A[q].Substring(C[zz + 1] , 1);
                            }
                            for (int kk = 0; kk < i; kk++)//جستجوی تمام قوانین برای یافتن سمت چپی که برابر با این متغیر حذف شده باشه
                                if (A[kk][0] == ss[C[zz] + 1])
                                {//اضافه کردن به جلوی قانونی که متغیر را حذف کردیم
                                    A[q] = A[q].Substring(0, A[q].Length - 1) + '|' + A[kk].Substring(2, A[kk].Length - 2);
                                }
                        }
                        zz++; x--;
                    }

                }

            } //print third rool
            
            listBox1.Items.Add("\n****hazve moteghayerhaye tanha****\n");
            for (int q = 0; q < i; q++)
            {
                if (A[q][1] != '=') A[q] = A[q].Substring(0, 1) + '=' + A[q].Substring(2, A[q].Length - 2);
                listBox1.Items.Add(A[q] + "\n"); }
        }
        //*********************************پایان قانون سوم*****

        //*************************************قانون چهارم
        private void button6_Click(object sender, EventArgs e)
        {
            int f = 0, count,g=0;
            char k = 'Q';
            int s = (int)k;
            for (int ff = 0; ff <= 9; ff++)//پر کردن آرایه کمکی
            {
                B[ff] = (char)s;
                s++;
            }
            k = '5';
            s = (int)k;
            for (int ff = 10; ff <= 14; ff++)
            {
                B[ff] = (char)s;
                s++;
            }
            while(A[g]!=null)
            {
                        int zz = 0;
                        for (int q = 0; q < i; q++)
                        {
                            int t = 0;
                            Array.Clear(C, 0, 100);
                            int jj = 2, r = 1, x = 0; int xx = A[q].Length; C[0] = 1;
                            zz = 0;
                            while (jj < xx)//مانند قانون سوم
                            {
                                if (A[q][jj] == '|' || A[q][jj] == ';')
                                { C[r++] = jj; x++; }
                                jj++;
                            }
                            while (zz < r)
                            {
                                if (C[zz + 1] == null) break;
                                else
                                {
                                    count = C[zz + 1] - C[zz];
                                    while (count > 3)
                                    {
                                        if (t > 0)
                                        {
                                            Array.Clear(C, 0, 100);
                                            jj = 2; r = 1; zz = 0; xx = A[q].Length; C[0] = 1;
                                            while (jj < xx)
                                            {
                                                if (A[q][jj] == '|' || A[q][jj] == ';')
                                                    C[r++] = jj;
                                                jj++;
                                            }
                                            if (C[zz + 1] - C[zz] <= 3) zz++;
                                        }
                                        if (x == 0) break;
                                        t++;
                                        A[i++] = B[f].ToString() + '='.ToString() + A[q].Substring(C[zz] + 1, 2) + ';';
                                        A[q] = A[q].Substring(0, C[zz] + 1) + B[f].ToString() + A[q].Substring(C[zz] + 3, A[q].Length - C[zz] - 3);
                                        f++;
                                        count--;
                                    }
                                    zz++;
                                }
                            }
                        } g++;
            }
            //print third rool

            listBox1.Items.Add("\n****kahesh motagherha be 2ta****\n");
            for (int q = 0; q < i; q++)
            {
                listBox1.Items.Add(A[q] + "\n");
            }
        }
        //*************************************پایان قانون چهارم

        //****************************************فرمت کردن آرایه ها برای مقداردهی دوباره
        private void button7_Click(object sender, EventArgs e)
        {
            //this.Refresh() ;
            Array.Clear(A, 0, 100);
            Array.Clear(B, 0, 7);
            Array.Clear(F, 0, 100);
            Array.Clear(C, 0, 100);
            Array.Clear(D,0,16);
            i = 0;
            listBox1.Items.Clear();
            
        }
        //*********************************************پایان فرمت


        //*********************************پویش
        //********************************************برای چک اینکه ببینیم رشته مورد نظر در چامسکی پذیرفته میشود یا نه 
        private void button8_Click(object sender, EventArgs e)
        {
           
            string[] sst=new string [100];
            int v=0,vv=0,n = textBox2.Text.Length;
            int[,] ih = new int[n, n];
            int xx=0;
            string s = textBox2.Text;
            for (int j = 0; j < i; j++)
            {

                string[] gr = A[j].Split('|', ';', '=');
                for (int jj = 1; jj < gr.Length - 1; jj++)//قسمت های مختلف یک قانون را جدا و دوباره ذخیره میکنیم
                    sst[xx++] = gr[0] + '=' + gr[jj];
            }
            string [,,] fg = new string[n, n,26];
           string sg=null;

            for(int yy=0;yy<n;yy++)//آرایه سه بعدی گرفتیم و بعد سوم را برای ذخیره سمت چپ هر قانونی که هر حرف رشته را دارد
                for (int aa = 0; aa < n; aa++)
                {
                    for (int r = 0; r < xx; r++)
                    {
                        if (sst[r][2] == s[aa])
                        {
                            v = (int)sst[r][0] - (int)'A';//چون در سی شارپ ایندکس های آرایه بصورت حروف نمی توان داشت از ای ترفند استفاده شد
                            fg[aa, aa, v] = sst[r][0].ToString();
                            break;
                            vv++;
                        }
                    }
                    if (vv > 0) { v = 0; break; }
                }
            int t=0;
            for(int l=1;l<=n;l++)//همان برنامه ضرب بهینه است برای دو ماتریس 
                for (int m = 0; m < n - l + 1; m++)
                {
                    t = m + l - 1;
                    for (int ll = m; ll < t; ll++)
                            for (int K = 0; K < 26; K++)
                        {
                            sg = null;
                            for (int L = 0; L < 26; L++)
                            {
                                sg = fg[m, ll, K] + fg[ll+1,t , L];//نقاط شکست
                                for (int tt = 0; tt < xx; tt++)
                                    if (sst[tt].Substring(2, sst[tt].Length - 2) == sg)//چک میکنیم ببینیم این قانون وجود دارد یا نه
                                    {
                                        v = (int)sst[tt][0] - (int)'A';
                                        fg[m, t, v] = sst[tt][0].ToString();
                                        ih[m, t] = ll+1;  //برای نقاط شکست درخت اشتقاق است
                                    }
                            }
                        }
                }
            //*************************************پایان پویش
            //**********************چاپ
            string message="   ||",eshteghagh="";
            for (int k = 0; k < n; k++)
            {
               for (int l = 0; l < n; l++)
                {
                    for (int r = 0; r < 26; r++)
                    {

                        if (fg[k, l, r] == null) continue;
                        
                         int u = 0;
                         for (int j = 0; j < 26;j++ )
                             if(fg[k, l, j]!=null)  u++;
                         if (u == 0)   message+="  ";
                         message += fg[k, l, r] + ',';
                    }
                    message += "   ||";
                }
               message += "\n";
              
            }
             
            Boolean sabet = false;
            for (int k = 0; k < 26; k++)
                if (fg[0, n - 1, k] == sst[0][0].ToString())
                {
                    message += "\n***پذیرفته شد " + textBox2.Text + " رشته***";
                    sabet = true;
                    break;
                }

            if (sabet == false)
            {
                message += "\n**پذیرفته نشد " + textBox2.Text + " رشته";
            } 
            message += "\n\nآیا می خواهید مکان های شکست برای درخت اشتقاق را ببینید؟";
            var result= MessageBox.Show(message, "پویش", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            if (result==DialogResult.Yes)
            {
                for (int k = 0; k < n; k++)
                {
                    for (int j = 0; j < n; j++)
                        eshteghagh += ih[k, j] + " ||";
                    eshteghagh += "\n";
                }
                eshteghagh+="\n\n\nIf You want to continue with another string or back to program click Yes\nAnd if you want to close program click No";
                var result2=MessageBox.Show(eshteghagh, "derivaion Tree", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                if (result2 == DialogResult.No)
                {
                    MessageBox.Show("Thank You For Using My program\n\nMohammad Khanjani","Good bye",MessageBoxButtons.OK,MessageBoxIcon.None);
                    this.Close();
                }
            }
            textBox2.Clear();
            Array.Clear(sst, 0, 100);
            Array.Clear(ih, 0, n);
            Array.Clear(fg, 0, n);
        }

        private void Help_Click(object sender, EventArgs e)
        {
           MessageBox.Show("لطفا در نوشتن قوانین توجه فرمایید که از حروف\n A تا F \nاستفاده فرمایید.و همین طور هر قانون باید با یک\n ;  \nوارد شود.و اینکه برای استفاده از تهی ، از $ استفاده کنید.به عنوان مثال \n A=BB|BA|CC|$;", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
