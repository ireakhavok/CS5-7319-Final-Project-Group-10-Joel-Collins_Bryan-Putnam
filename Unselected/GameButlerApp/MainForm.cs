using DAL_API;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;

namespace SoftwareArchitectureWinformsApp
{
    public partial class MainForm : Form
    {
        public AuthForm authformhandle;
        public LoginForm loginFormHandle;
        public SteamLogin steamLoginHandle;
        public string steamID;
        private SteamController steamController = new SteamController();
        IEnumerable<Cookie> cookies = new Cookie[] { };

        public MainForm()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (File.Exists(appDataFolder + "\\GameButler\\apiKey.txt"))
            {
                StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler\\apiKey.txt");
                while (!reader.EndOfStream)
                {
                    string apiKey = reader.ReadLine();
                    steamController.apiKey = apiKey;

                }
            }
            else
            {
                authformhandle = new AuthForm(AuthForm.typeOfForm.APIkey);
                authformhandle.ShowDialog();
                if (authformhandle.DialogResult == DialogResult.OK)
                {
                    steamController.apiKey = authformhandle._code;
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler");
                    }
                    var writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler\\apiKey.txt");
                    writer.WriteLine(steamController.apiKey);
                    writer.Close();
                }
            }
            if (File.Exists(appDataFolder + "\\GameButler\\cookies.txt"))
            {

                StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler\\cookies.txt");
                while (!reader.EndOfStream)
                {
                    string cookie = reader.ReadLine();
                    //cookies.Add(cookie);
                    Regex rx1 = new Regex(@"([a-zA-Z_]*)=");
                    Regex rx2 = new Regex(@"=(.*$)");
                    MatchCollection matche1 = rx1.Matches(cookie);
                    MatchCollection matche2 = rx2.Matches(cookie);
                    Cookie loginCookie = new Cookie(matche1[0].Groups[1].Value, matche2[0].Groups[1].Value) { Domain = "steampowered.com" };
                    cookies = cookies.Append(loginCookie);
                }

                //ATTEMPT 1 WORKS
                steamLoginHandle = new SteamLogin();
                steamLoginHandle.Authenticate(cookies);
                bool verified = steamLoginHandle.VerifyCookies();
                Cookie steamIDcookie = cookies.FirstOrDefault(n => n.Name == "steamLoginSecure");
                Cookie sessionkey = cookies.FirstOrDefault(n => n.Name == "auth");
                Cookie encrypted_loginkey = cookies.FirstOrDefault(n => n.Name == "token_secure");

                Regex re = new Regex("([0-9]*)%");
                Match match1 = re.Match(steamIDcookie.Value);
                steamID = match1.Groups[1].Value;

                //steamLogin.getSessionKey();
                //string steamid, string sessionkey, string encrypted_loginkey
                //ERROR 403 FORBIDDEN
                //steamWeb.AuthenticateUser(steamIDcookie.Value, sessionkey.Value, encrypted_loginkey.Value);


            }
            else
            {
                loginFormHandle = new LoginForm();
                loginFormHandle.ShowDialog();

                steamID = loginFormHandle.userID;
            }
            ColorScheme scheme = new ColorScheme();
            InitializeComponent();
            ChangeTheme(scheme, this.Controls);

        }

        //public event EventHandler webViewInitialized;

        //public delegate void loadCookies(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs);
        public void loadCookies()
        {
            //steamController.getAllApps;
            //string uri = @"https://store.steampowered.com/";
            //ADDING COOKIES
            foreach (Cookie currentCookie in cookies)
            {
                var cookie = WebViewSidebar.CoreWebView2.CookieManager.CreateCookie(currentCookie.Name, currentCookie.Value, ".steampowered.com", null);
                cookie.IsHttpOnly = true;
                cookie.IsSecure = true;
                WebViewSidebar.CoreWebView2.CookieManager.AddOrUpdateCookie(cookie);
            }
            //WebViewSidebar.CoreWebView2.Navigate(uri);
        }

        public void ChangeTheme(ColorScheme scheme, Control.ControlCollection container)
        {
            foreach (Control component in container)
            {
                if (component is Panel)
                {
                    ChangeTheme(scheme, component.Controls);
                    component.BackColor = scheme.PanelBG;
                    component.ForeColor = scheme.PanelFG;
                }
                else if (component is Button)
                {
                    //component.BackColor = scheme.ButtonBG;"{Name=ControlDark, ARGB=(255, 160, 160, 160)}"
                    //component.ForeColor = scheme.ButtonFG;
                }
                else if (component is TextBox)
                {
                    component.BackColor = scheme.TextBoxBG;
                    component.ForeColor = scheme.TextBoxFG;
                }

            }
        }
        public BindingSource bindingSource = new BindingSource();

        private void refreshGrid()
        {

            this.GameGrid.DataSource = (IList<PlayerGame>?)steamController.gameList.response.games.OrderByDescending(x => x.LastDatePlayed).ToList();
            this.GameGrid.Update();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (checkBoxUseMultithreading.Checked)
            {
                try
                {
                    this.GameGrid.DataBindings.Clear();
                    button.Enabled = false;

                    await steamController.GetGames(steamID);
                    await steamController.getInfoForGamesAsync();

                    Thread[] threads = new Thread[2];
                    threads[0] = new Thread(new ThreadStart(steamController.getPlayerCountForGamesAsync));
                    threads[0].Start();
                    threads[1] = new Thread(new ThreadStart(steamController.getNewsDatesForGamesAsync));
                    threads[1].Start();

                    for (int i = 0; i < threads.Length; i++)
                    {
                        threads[i].Join();
                    }
                    button.Visible = false;
                    refreshGrid();
                }
                catch
                {
                    button.Enabled = true;
                }
            }
            else
            {
                try
                {
                    this.GameGrid.DataBindings.Clear();
                    button.Enabled = false;

                    steamController.GetGamesNonAsync(steamID);
                    steamController.getInfoForGamesNonAsync();
                    steamController.getPlayerCountForGamesNonAsync();
                    steamController.getNewsDatesForGamesNonAsync();
                    button.Visible = false;
                    refreshGrid();
                }
                catch
                {
                    button.Enabled = true;
                }
            }

        }

        private bool SiteExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (true);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        private void GameGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (e.ColumnIndex < 0)
            {

            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var playerGameObject = (PlayerGame)(senderGrid.Rows[e.RowIndex].DataBoundItem);
                if (senderGrid.Columns[e.ColumnIndex].HeaderText == "PlayerCount")
                {
                    var url = "https://steamcharts.com/app/" + playerGameObject.appid.ToString();
                    if (SiteExists(url))
                    {
                        this.WebViewSidebar.Source = new System.Uri(url);
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex].HeaderText == "Name")
                {
                    var url = "https://store.steampowered.com/app/" + playerGameObject.appid.ToString();
                    if (SiteExists(url))
                    {
                        this.WebViewSidebar.Source = new System.Uri(url);
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex].HeaderText == "LastNews")
                {
                    var url = "https://store.steampowered.com/news/app/" + playerGameObject.appid.ToString();
                    if (SiteExists(url))
                    {
                        this.WebViewSidebar.Source = new System.Uri(url);
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex].HeaderText == "Metacritic")
                {
                    var url = playerGameObject.MetacriticURL;
                    if (SiteExists(url))
                    {
                        this.WebViewSidebar.Source = new System.Uri(url);
                    }
                }
            }
            else if (e.RowIndex < 0)
            {
                if (this.GameGrid.Columns[e.ColumnIndex].DataPropertyName == "PlayerCount")
                    this.GameGrid.DataSource = (IList<PlayerGame>?)steamController.gameList.response.games.OrderByDescending(x => x.PlayerCount).ToList();
                if (this.GameGrid.Columns[e.ColumnIndex].DataPropertyName == "Name")
                    this.GameGrid.DataSource = (IList<PlayerGame>?)steamController.gameList.response.games.OrderBy(x => x.Name).ToList();
                if (this.GameGrid.Columns[e.ColumnIndex].DataPropertyName == "Playtime")
                    this.GameGrid.DataSource = (IList<PlayerGame>?)steamController.gameList.response.games.OrderByDescending(x => x.playtime_forever).ToList();
                if (this.GameGrid.Columns[e.ColumnIndex].DataPropertyName == "LastNews")
                    this.GameGrid.DataSource = (IList<PlayerGame>?)steamController.gameList.response.games.OrderByDescending(x => x.LastNews).ToList();
                if (this.GameGrid.Columns[e.ColumnIndex].DataPropertyName == "LastDatePlayed")
                    this.GameGrid.DataSource = (IList<PlayerGame>?)steamController.gameList.response.games.OrderByDescending(x => x.LastDatePlayed).ToList();
                if (this.GameGrid.Columns[e.ColumnIndex].DataPropertyName == "MetacriticScore")
                    this.GameGrid.DataSource = (IList<PlayerGame>?)steamController.gameList.response.games.OrderByDescending(x => x.MetacriticScore).ToList();

                this.GameGrid.Update();

            }
        }

        private void WebViewSidebar_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            loadCookies();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox objectCheckbox = (CheckedListBox)sender;

            for (int i = 0; i < objectCheckbox.Items.Count; i++)
            {
                if (objectCheckbox.GetItemChecked(i))
                {
                    this.GameGrid.Columns[i + 1].Visible = false;
                }
                else
                {
                    this.GameGrid.Columns[i + 1].Visible = true;

                }
            }
        }
    }
}