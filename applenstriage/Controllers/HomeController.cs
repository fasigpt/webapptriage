using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace applenstriage.Controllers
{
    public class ServerClass
    {
        const int MIN_ITERATIONS = int.MaxValue / 1000;
        const int MAX_ITERATIONS = MIN_ITERATIONS + 10000;

        long m_totalIterations = 0;
        readonly object m_totalItersLock = new object();
        // The method that will be called when the thread is started.
        public void DoWork()
        {
           // Console.WriteLine(
               // "ServerClass.InstanceMethod is running on another thread.");

            var x = GetNumber();
        }

        private int GetNumber()
        {
            var rand = new Random();
            var iters = rand.Next(MIN_ITERATIONS, MAX_ITERATIONS);
            var result = 0;
            lock (m_totalItersLock)
            {
                m_totalIterations += iters;
            }
            // we're just spinning here
            // and using Random to frustrate compiler optimizations
            for (var i = 0; i < iters; i++)
            {
                result = rand.Next();
            }
            return result;
        }
    }
        public class HomeController : Controller
    {
        public ActionResult Index()
        {
                     

            return View();
        }

        public ActionResult Memory()
        {
            ArrayList list = new ArrayList();
            int i = 0;
            while (true)
            {
                byte[] newBt = new byte[1024 * 1024 * 10];
                
                    list.Add(newBt); // 10 MB
                    i += 10;
                    //Console.WriteLine(i);
                
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult cpu()
        {
            for (int i = 0; i < 2001; i++)
            {
                CreateThreads();
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public static void CreateThreads()
        {
            ServerClass serverObject = new ServerClass();

            Thread InstanceCaller = new Thread(new ThreadStart(serverObject.DoWork));
            // Start the thread.
            InstanceCaller.Start();

            

        }

        public async Task<ActionResult> Slow()
        {
            Uri ourUri = new Uri("http://as/Home/About");
            WebRequest myWebRequest = WebRequest.Create(ourUri);
            var myWebResponse = await myWebRequest.GetResponseAsync();
           // await myWebRequest;
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HandleError(View = "Index")]
        public ActionResult Crash()
        {
            ViewBag.Message = $"Your contact {"page"}.";

            Foo();

            return View();
        }

        private void Foo()
        {
            Foo();
        }
    }
}