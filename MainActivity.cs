using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Collections.Generic;
using System.Threading;
using RestSharp;
using System;
using Newtonsoft.Json;
using Threads.API;
using Threads.Models;
using System.Threading.Tasks;
using System.Net;
using static Threads.MainActivity;
using System.Net.Http;

namespace Threads
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Variaveis

        // gerais
        string TipoVeiculo = "2";
        string MarcaId = "80";

        // main
        Button  buttonAperte  = null;
        Button  segundoBotao  = null;
        Spinner spinnerMarca  = null;
        Spinner spinnerAno    = null;
        Spinner spinnerModelo = null;

        #endregion

        #region Override

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                
            ExibeLayoutMain();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion

        #region Telas e Layouts
          
        private void ExibeLayoutMain()
        {
            SetContentView(Resource.Layout.activity_main);

            buttonAperte  = FindViewById<Button> (Resource.Id.buttonAperte);
            segundoBotao  = FindViewById<Button> (Resource.Id.segundoBotao);
            spinnerMarca  = FindViewById<Spinner>(Resource.Id.spinnerMarca);
            spinnerAno    = FindViewById<Spinner>(Resource.Id.spinnerAno);
            spinnerModelo = FindViewById<Spinner>(Resource.Id.spinnerModelo);

            // clicks
            //buttonAperte.Click += (sender, e) => {
            //    Thread t = new Thread(() => { MetodoPesado(10); });
            //    t.Start();
            //};

            segundoBotao.Click += (sender, e) => {
                ListarMarcas();
            };

        }

        #endregion

        #region Metodos

        private void MetodoPesado(int comeco)
        {
            long teste = comeco;

            for (int i = 0; i < 9990F; i++)
            {
                for (int j = 0; j < 8888M; j++)                
                    teste += j;                
            }
        }

        #endregion

        #region item Click

        private void Spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            SpinnerMarcaAdapter adapter = (sender as Spinner).Adapter as SpinnerMarcaAdapter;

            List<MarcasResponse> marcas = adapter.Items;

            MarcaId = marcas[e.Position].Id.ToString();

            ListarModelos();
        }

        private void SpinnerModelo_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Requisicoes

        private void ListarMarcas()
        {
            Task.Run(() =>
            {
                try
                {
                    List<MarcasResponse> MarcasVeiculos = JsonConvert.DeserializeObject<List<MarcasResponse>>(
                        RouteManager.ListarMarcas(new MarcasRequest(), TipoVeiculo).Content
                    );

                    SpinnerMarcaAdapter spinnerAdapter = new SpinnerMarcaAdapter(this, MarcasVeiculos);

                    RunOnUiThread(() =>
                    {
                        spinnerMarca.Adapter = spinnerAdapter;
                        spinnerMarca.ItemSelected += Spinner1_ItemSelected;
                    });                    
                }
                catch (Exception ex)
                {
                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
                    });
                }
            });
        }

        private void ListarModelos()
        {
            Task.Run(() =>
            {
                try
                {
                    List<ModelosResponse> ModelosMarca = JsonConvert.DeserializeObject<List<ModelosResponse>>(
                        RouteManager.ListarModelos(new ModelosRequest(), MarcaId).Content
                    );

                    SpinnerModeloAdapter spinnerAdapter = new SpinnerModeloAdapter(this, ModelosMarca);

                    RunOnUiThread(() =>
                    {
                        spinnerModelo.Adapter       = spinnerAdapter;
                        spinnerModelo.ItemSelected += SpinnerModelo_ItemSelected; ;
                    });
                }
                catch (Exception ex)
                {
                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
                    });
                }
            });
        }

        #endregion

        #region Comum

        public class SpinnerMarcaAdapter : ArrayAdapter<MarcasResponse>
        {
            private LayoutInflater inflater;
            public List<MarcasResponse> Items;

            public SpinnerMarcaAdapter(Context context, List<MarcasResponse> Marcas) : base(context, 0, Marcas)
            {
                inflater   = LayoutInflater.From(context);
                this.Items = Marcas;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                view ??= inflater.Inflate(Resource.Layout.TabelaFipe_ConsultaVeiculo_Row, null);

                MarcasResponse marca = GetItem(position);
                if (marca != null)
                    view.FindViewById<TextView>(Resource.Id.TabelaFipe_ConsultaVeiculo_Row_txtNomeCarro).Text = marca.Brand;

                return view;
            }

            public override View GetDropDownView(int position, View convertView, ViewGroup parent)
            {
                return GetView(position, convertView, parent);
            }
        }

        public class SpinnerModeloAdapter : ArrayAdapter<ModelosResponse>
        {
            private LayoutInflater inflater;

            public SpinnerModeloAdapter(Context context, List<ModelosResponse> Marcas) : base(context, 0, Marcas)
            {
                inflater = LayoutInflater.From(context);
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                view ??= inflater.Inflate(Resource.Layout.TabelaFipe_ConsultaVeiculo_Modelos_Row, null);

                ModelosResponse modelo = GetItem(position);
                if (modelo != null)
                {
                    view.FindViewById<TextView>(Resource.Id.TabelaFipe_ConsultaVeiculo_Modelos_Row_txtNomeModelo).Text = modelo.Model;
                    view.FindViewById<TextView>(Resource.Id.TabelaFipe_ConsultaVeiculo_Modelos_Row_txtAno).Text = modelo.Years;
                }                   

                return view;
            }

            public override View GetDropDownView(int position, View convertView, ViewGroup parent) => GetView(position, convertView, parent);            
        }

        #endregion
    }
}