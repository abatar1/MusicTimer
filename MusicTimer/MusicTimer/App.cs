using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicTimer.Domain;
using MusicTimer.Pages;
using Xamarin.Forms;

namespace MusicTimer
{
    public class App : Application
    {
        public App()
        {
            // TODO Написать оболочку на Palletizer, которая будет инициализировать его из базы данных
            // TODO Синхронизировать данные 
            // TODO При запуске проверять на наличие новых треков и обновлять базу данных
            // TODO Ну и саму базу данных неплохо бы завести
            MainPage = new NavigationPage(new MainPage(new Palletizer()));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
