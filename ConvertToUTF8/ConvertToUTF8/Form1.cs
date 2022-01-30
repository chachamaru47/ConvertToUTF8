namespace ConvertToUTF8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            string[] files = textBox2.Text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            string[] exts = textBox1.Text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            textBox2.Text = string.Empty;
            foreach (string file in files)
            {
                if (string.IsNullOrEmpty(file)) { continue; }

                string f = file.Remove(0, 1);
                bool result = AkSystem.EncodeConverter.ConvertToUTF8(f, exts);
                textBox2.Text += (result ? "Åú" : "Å@") + f + Environment.NewLine;
                if (result) { count++; }
            }
            MessageBox.Show($"Converted {count} file.");
        }

        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = new string[0];
            if (e.Data != null)
            {
                paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            }

            textBox2.Text = string.Empty;
            var files = new List<string>();
            foreach (string path in paths)
            {
                if (Directory.Exists(path))
                {
                    files.AddRange(Directory.GetFiles(path, "*", SearchOption.AllDirectories));
                }
                else
                {
                    files.Add(path);
                }
            }
            files.ForEach(file => textBox2.Text += "Å@" + file + Environment.NewLine);
        }

        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
            }
        }
    }
}