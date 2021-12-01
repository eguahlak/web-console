using System;
using System.Threading;
using WebConsoleConnector.Form;
using static WebConsoleConnector.Utilities.Extensions;


namespace TemplateBackend
{

    public class LtoPanel : Panel
    {
        public Label TapeLabel { get; }
        public Field TapeNote { get; }
        public Button DoButton { get; }
        public Button DontButton { get; }
        public Button ClearButton { get; }

        public LtoPanel(string text, string extra) : base() {
            TapeLabel = new Label(text) { Width = 20 };
            TapeNote = new Field(extra, $"Enter note on {text} here", true) { Width = 50 };
            DoButton = new("Do")
            {
                OnClick = c =>
                {
                    DontButton.Disabled = false;
                    (c as Button).Disabled = true;
                }
            };
            DontButton = new("Don't")
            {
                Disabled = true,
                OnClick = c =>
                {
                    DoButton.Disabled = false;
                    ClearButton.Disabled = false;
                    (c as Button).Disabled = true;
                }
            };
            ClearButton = new("*C*l/e/_a_r")
            {
                Disabled = true,
                OnClick = c =>
                {
                    Hidden = true;
                }
            };
            Add(TapeLabel);
            Add(TapeNote);
            Add(DoButton);
            Add(DontButton);
            Add(ClearButton);

        }
    }

    class Program
    {

        private static void TryForm()
        {
            var label = new Label("Her /starter/ det");
            var field = new Field("", "Skriv noget her", true)
            {
                OnUpdate = (c, v) =>
                {
                    label.Value = $"Last value of {c.Id} was: {v}";
                }
            };
            var ltoPanel = new Panel(true);


            var myForm = new HttpForm("Hello Nico and Frede")
            {
                new Text("*A*utomatiseret *L*angtidsbevaring og *M*odtagelse af *A*rkivalier", 1),
                new Text("Media Copy Instrument", 2),
                Newline.Ruler,
                label,
                field,
                // Newline.Break,
                Newline.Ruler,
                new Text("Dette er en `text` med *fed og _understreget /kursiv/_*! **-quality, a//b + c//d = (a**d + c**b)//(b**d)"),
                new Button("Tryk her!")
                {
                    OnClick = c =>
                    {
                        var elem = new LtoPanel("LTO", "bare en test");
                        ltoPanel.Insert(elem);
                    }
                },
                ltoPanel
            };
            myForm.Publish();

            Console.WriteLine("published");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(10_000);
                ltoPanel.Insert(new LtoPanel($"LT0000{i}", ""));
            }

        }

        static void Main(string[] args)
        {
            // TryFormPublish();
            TryForm();
        }
    }
}
