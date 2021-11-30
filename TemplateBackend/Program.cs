using System;
using System.Threading;
using WebConsoleConnector.Form;
using static WebConsoleConnector.Utilities.Extensions;


namespace TemplateBackend
{

    public class LtoPanel : Panel
    {
        public LtoPanel(string text, string extra) : base() {
            var label = new Label(text);
            Add(label);
            Add(new Field("", $"Enter note on {text} here", true));
            Add(new Button("Done!") 
            {
                OnClick = c =>
                { 
                    label.Value = $"*{label.Value}*";
                }
            });
            }
    }

    class Program
    {

        private static void TryFormPublish()
        {
            var someLabel = new Label("Location: ");
            var someText = new Field("We were here", false);
            var someField = new Field("Change me", true)
            {
                OnUpdate = (c, v) =>
                {
                    Console.WriteLine($">>{c.Id} was updated to {v}");
                    someText.Value = $"We were here with {v}";
                }
            };
            var mediaPanel = new Panel
            {
                new Label("LT0001")
            };
            var frederikButton = new Button("Hej Frederik")
            {
                OnClick = (c) =>
                {
                    someText.Value = "";
                }
            };
            var form = new HttpForm("Copy Instrument")
            {

                someLabel,
                someText,
                someField,
                frederikButton,
                new Button("Press when done"),
                new Panel()
                {
                    new Button("Continue"),
                    new Button("Cancel")

                },
                mediaPanel
            };
            form.Publish(8089);
            for (int i = 1; true; i++)
            {
                // Console.WriteLine("Indtast en tekst");
                string text = Console.ReadLine();
                someText.Value = text;
                someLabel.Value = $"{i}. placering";
                mediaPanel.Add(new Label(text));
            }
        }


        private static void TryForNicoAndFrede()
        {
            var label = new Label("Her starter det");
            var field = new Field("Skriv noget her", true)
            {
                OnUpdate = (c, v) =>
                {
                    label.Value = $"Last value of {c.Id} was: {v}";
                }
            };
            var ltoPanel = new Panel();


            var myForm = new HttpForm("Hello Nico and Frede")
            {
                label,
                field,
                new Button("Tryk for helvede!")
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
                Thread.Sleep(2_000);
                ltoPanel.Insert(new LtoPanel($"LT0000{i}", "Please copy"));
            }

        }

        static void Main(string[] args)
        {
            // TryFormPublish();
            TryForNicoAndFrede();
        }
    }
}
