using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Compression;
using System.Threading;




namespace TheBillionApp
{

    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<empresa> empresas;
        public string getroute1, getroute2, getnewroute;
        public int totalIntervalos, seleccionado = -1, seleccionLista = -1, opt = 0;
        public List<string> rutas;
        

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            empresas = new List<empresa>();
            rutas = new List<string>();



            Fileroute fr = new Fileroute(this);
            fr.Show();
            // AdminPanel ap = new AdminPanel();
            //ap.Show();

            this.Hide();

         

            /*
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();*/

        }


        public void getfileroutes()
        {
            getroute1 = Fileroute.route1;
            getroute2 = Fileroute.route2;
            getnewroute = Fileroute.newroute;
           

            rutas.Add(@getroute1);
            rutas.Add(@getroute2);
            cargando();



        }

        public void colorear()
        {
           
        }

        public void cambioLlenado(int a)
        {


            opt = a;
          
                string t1 = empresas[tabla.SelectedIndex].intervaloMal[seleccionLista];
                string[] tt = t1.Split('-');
                t1 = tt[0];
                string t2 = tt[1];
                List<lectura> lec = empresas[tabla.SelectedIndex].getLectura();
                List<lectura> tem = new List<lectura>();

                if (opt == 1)
                {

                    foreach (lectura l1 in lec)
                    {


                        if (l1.fecha.Equals(t1))
                        {
                            l1.cantidad = 0;

                        }
                        else
                        if (l1.fecha.Equals(t2))
                        {
                            l1.cantidad = 0;
                            tem.Add(l1);
                            break;
                        }

                        // tem.Add(l1);
                    }
                    // empresas[tabla.SelectedIndex].setListaLectura(tem);
                }
                if (opt == 2)
                {

                    foreach (lectura li2 in lec)
                    {
                        if (li2.index == seleccionLista)
                        {
                            t1 = li2.fecha;

                            string te = t1.Substring(0, 2);
                            int dia = 0;
                            int.TryParse(te, out dia);
                            int pos = -1, pos2 = 0;
                            //buscamos dia anterior
                            if (dia < 9)
                                dia = dia + 7;
                            else
                                dia = dia - 7;
                            String fecTem = "";
                            if (dia < 10)
                                fecTem = "0" + dia.ToString();
                            else
                                fecTem = dia.ToString();
                            fecTem = fecTem + t1.Substring(2);

                            foreach (lectura li in lec)
                            {
                                pos++;

                                if (li.fecha.Equals(t1))
                                {
                                    pos2 = pos;
                                }

                            }

                            foreach (lectura l1 in lec)
                            {

                                if (l1.fecha.Equals(fecTem))
                                {
                                    float nCantidad = l1.cantidad;
                                    empresas[tabla.SelectedIndex].lista[pos2].cantidad = nCantidad;
                                    empresas[tabla.SelectedIndex].lista[pos2].e = l1.e;
                                    empresas[tabla.SelectedIndex].lista[pos2].pm = l1.pm;
                                    empresas[tabla.SelectedIndex].lista[pos2].q1 = l1.q1;
                                    empresas[tabla.SelectedIndex].lista[pos2].q2 = l1.q2;
                                    empresas[tabla.SelectedIndex].lista[pos2].q3 = l1.q3;
                                    empresas[tabla.SelectedIndex].lista[pos2].q4 = l1.q4;
                                    empresas[tabla.SelectedIndex].lista[pos2].r = l1.r;
                            

                                }

                            }


                        }
                    
                }

            }//if

        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            cargando();
            dispatcherTimer.Stop();

        }

        private void cargando()
        {
            int con = 0;

            foreach (string r in rutas)
            {
                con++;
                getEmpesas(@r, con);

            }

                datos();
                getFecha();
                //ver();
                llenado();

            }

            private void generar()
            {
                generarExcel m = new generarExcel(empresas);

                m.generarEmpresa(empresas[seleccionado]);

            }
            public void ver()
            {

                string l = "";
                empresa tem = empresas[0];
                MessageBox.Show(tem.nombre);
            }
            private void llenado()
            {//

                tabla.ItemsSource = empresas;
            }


            void MakeButton(object sender, RoutedEventArgs e)
            {
                seleccionado = tabla.SelectedIndex;


                Button btn = (Button)e.Source;
                btn.IsEnabled = false;



                Thread te = new Thread(generar);
                te.Start();

            }



            private void datos()
            {//Local Time	kVARh Q1	kVARh Q4	kWh del E
             /* falta case para cuando la empresa no tienen todas las columnas o no tienen ningun registro*/
                string r1 = rutas[0];
                string r2 = rutas[1];


                int conta = 0, conta2 = -1;
                int columnas = 0;
                try
                {
                    foreach (empresa fila in empresas)
                    {
                        var conn = new OleDbConnection();
                        var cmd = new OleDbCommand();
                        var da = new OleDbDataAdapter();
                        var ds = new DataSet();
                        conta2++;
                        int indice = -1, indice2 = -1;
                        try
                        {

                            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + (fila.index == 1 ? r1 : r2) + ";Mode=Read;Extended Properties=Excel 8.0;Persist Security Info=False;";
                            int danado = 0;
                            cmd.CommandText = "SELECT * FROM [" + fila.clave + "$]"; // no olivdar incluir el simbolo de peso
                            cmd.Connection = conn;
                            da.SelectCommand = cmd;
                            conn.Open();
                            da.Fill(ds);
                            string rango = "", rangoAnterior = "";

                            Boolean existeRango = false, existeRango2 = false;
                            int total = ds.Tables[0].Columns.Count;
                            fila.columnas = total;
                        DataRow tem = null;


                            foreach (DataRow fila2 in ds.Tables[0].Rows)
                            {


                                float t = 0.0f, t1 = 0.0f, t2 = 0.0f, t3 = 0.0f, t4 = 0.0f, t5 = 0.0f, t6 = 0.0f, t7 = 0.0f;
                                if (total == 5)
                                {
                                    if (!float.TryParse(fila2[4].ToString(), out t))
                                        t = -1;

                                    if (!float.TryParse(fila2[2].ToString(), out t2))
                                        t2 = 0;
                                    if (!float.TryParse(fila2[3].ToString(), out t3))
                                        t3 = 0;

                                }
                                if (total == 8)
                                {
                                    if (!float.TryParse(fila2[6].ToString(), out t))
                                        t = -1;
                                    if (!float.TryParse(fila2[2].ToString(), out t2))
                                        t2 = 0;
                                    if (!float.TryParse(fila2[3].ToString(), out t3))
                                        t3 = 0;
                                    if (!float.TryParse(fila2[4].ToString(), out t4))
                                        t4 = 0;
                                    if (!float.TryParse(fila2[5].ToString(), out t5))
                                        t5 = 0;
                                    if (!float.TryParse(fila2[7].ToString(), out t6))
                                        t6 = 0;

                                }
                                if (total == 9)
                                {
                                    if (!float.TryParse(fila2[6].ToString(), out t))
                                        t = -1;


                                    if (!float.TryParse(fila2[2].ToString(), out t2))
                                        t2 = 0;
                                    if (!float.TryParse(fila2[3].ToString(), out t3))
                                        t3 = 0;
                                    if (!float.TryParse(fila2[4].ToString(), out t4))
                                        t4 = 0;
                                    if (!float.TryParse(fila2[5].ToString(), out t5))
                                        t5 = 0;
                                    if (!float.TryParse(fila2[7].ToString(), out t6))
                                        t6 = 0;
                                    if (!float.TryParse(fila2[8].ToString(), out t6))
                                        t7 = 0;
                                }

                                if (t == -1)
                                {
                                    if (rango == "")
                                    {

                                        indice++;
                                        rango += fila2[0].ToString();
                                        existeRango = true;
                                    }
                                    else
                                        rangoAnterior = fila2[0].ToString();

                                    danado++;
                                tem = fila2;


                                }
                                if (t > 0 && existeRango == true)
                                {
                                    if (rangoAnterior == "")
                                        rango += "-" + fila2[0].ToString();
                                    else
                                        rango += "-" + rangoAnterior;
                                    rangoAnterior = "";
                                    fila.addIntervaloMal(rango);

                                    rango = "";
                                    rangoAnterior = "";
                                    existeRango = false;
                                }
                                if (t == -1)
                                {
                                    t = 0;
                                    indice2 = indice;

                                }
                                else
                                    indice2 = -1;

                                if (total == 8)
                                    fila.setLectura(new lectura(fila2[0].ToString(), t, t2, t3, t4, t5, t6, indice2));
                                if (total == 9)
                                    fila.setLectura(new lectura(fila2[0].ToString(), t, t2, t3, t4, t5, t6, t7, indice2));
                                if (total == 5)
                                    fila.setLectura(new lectura(fila2[0].ToString(), t, t2, t3, indice2));



                            }
                        if ( existeRango == true)
                        {
                            if (rangoAnterior == "")
                                rango += "-" + tem[0].ToString();
                            else
                                rango += "-" + rangoAnterior;
                            rangoAnterior = "";
                            fila.addIntervaloMal(rango);

                            rango = "";
                            rangoAnterior = "";
                            existeRango = false;
                            tem = null;
                        }
                        fila.totalDanado = danado;
                        }

                        catch (Exception ex)
                        {
                            //columna 6  
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                        conn = null;
                        cmd = null;
                        da = null;
                        ds = null;
                        indice = -1;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }


            }


            private Boolean existe(string termino)
            {
                int pos = -1;
                int conta = -1;

                foreach (empresa e in empresas)
                {
                    conta++;
                    if (e.clave == termino)
                        return true;


                }


                return false;
            }

            public void getEmpesas(string path, int index)
            {

                var conn = new OleDbConnection();
                var cmd = new OleDbCommand();
                var da = new OleDbDataAdapter();
                var ds = new DataSet();
                string indice, nombre, m = "";
                try
                {

                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Mode=Read;Extended Properties=Excel 8.0;Persist Security Info=False;";
                    cmd.CommandText = "SELECT * FROM [|PRIVATE|Status$]"; // no olivdar incluir el simbolo de peso
                    cmd.Connection = conn;
                    da.SelectCommand = cmd;
                    conn.Open();
                    da.Fill(ds);
                    foreach (DataRow fila2 in ds.Tables[0].Rows)
                    {
                        String[] substrings = fila2[1].ToString().Split(':');
                        if (!substrings[0].Equals("Starting report"))
                        {
                            indice = substrings[0];
                            if (substrings.Length > 1)
                            {
                                if (substrings[1].Contains("<")) {
                                    String[] ex = substrings[1].ToString().Split('<');
                                    String[] ex2 = ex[1].ToString().Split('>');
                                    if (!existe(indice))
                                        empresas.Add(new empresa(indice, ex2[0], index));
                                    m += indice + "\n";
                                }
                            }


                        }





                    }
                    //   MessageBox.Show(m);
                    // datos(path);


                }
                catch (Exception er) { MessageBox.Show(er.ToString()); }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            private void generarxls(object sender, RoutedEventArgs e)
            {
                seleccionado = tabla.SelectedIndex;


                Button btn = (Button)e.Source;
                btn.IsEnabled = false;
                Thread te = new Thread(generar);
                te.Start();
            }

            private void generacsv(object sender, RoutedEventArgs e)
            {
                seleccionado = tabla.SelectedIndex;


                Button btn = (Button)e.Source;
                btn.IsEnabled = false;

                Thread te = new Thread(generacsv1);
                te.Start();
            }

            private void generacsv1()
            {
                generarCSV m = new generarCSV();

                Boolean b1 = m.generar(empresas[seleccionado]);
            }

        private void buscar(object sender, KeyEventArgs e)
        {

            
         
      
        }

        private void generaTodo(object sender, RoutedEventArgs e)
        {
            /*  seleccionado = tabla.SelectedIndex;
              Button btn = (Button)e.Source;
              btn.IsEnabled = false;
              Thread te = new Thread(generaTODOA);
              te.Start();*/
            generador x = new generador(this);
            this.Hide();
            x.Show();
        }

        private void verGrafica(object sender, RoutedEventArgs e)
        {
            seleccionLista = tabla.SelectedIndex;
           grafica x = new grafica(empresas[seleccionLista]);
            x.Show();

        }

        private void aBuscar(object sender, RoutedEventArgs e)
        {
            int index = -1;

            foreach (empresa em in empresas)
            {
                index++;

                if (em.clave.Contains(busca.Text))
                {
                    try
                    {

                        tabla.SelectedIndex = index;
                        DataGridRow row = (DataGridRow)tabla.ItemContainerGenerator.ContainerFromIndex(index);
                        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                    catch (Exception er) { }



                }
            }
        }

        private void tabla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void generaTODOA()
        {/*
            generarDBF m = new generarDBF();
            m.generar(empresas);
            generarCSV t1 = new generarCSV();
            t1.generar(empresas);
            generarExcel t2 =new generarExcel(empresas);
            t2.generarEmpresa(empresas);*/
        }

            private void generadbf(object sender, RoutedEventArgs e)
            {
                seleccionado = tabla.SelectedIndex;
                Button btn = (Button)e.Source;
                btn.IsEnabled = false;
                Thread te = new Thread(generaDBF);
                te.Start();
            }

            private void generaDBF()
            {
                generarDBF m = new generarDBF();
                m.generar(empresas[seleccionado]);
            }

            private void cambio(object sender, MouseButtonEventArgs e)
            {
          
                ListView btn = (ListView)sender;
                seleccionLista = btn.SelectedIndex;
                opcion opc = new opcion(this);
                opc.Show();
            
            }

            public void getFecha()
            {
                string fecha;
                fecha = "";
                lectura tem = empresas[0].getElementoLectura(0);
                fecha = tem.fecha;
                string[] f = fecha.Split('/');
                string[] m = f[2].Split(' ');
                int a, b;
                int.TryParse(m[0], out b);
                int.TryParse(f[1], out a);
                int dias = System.DateTime.DaysInMonth(b, a);
                totalIntervalos = (((60 / 5) * 24) * dias);


            }

           
        }
    }



