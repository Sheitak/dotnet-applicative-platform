using TerminalApp.Forms;

namespace TerminalApp
{
    public partial class TerminalForm : Form
    {
        public TerminalForm()
        {
            InitializeComponent();
            CenterToScreen();

            dateLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            ScannerForm scanForm = new ScannerForm();
            scanForm.ShowDialog();
        }
    }
}