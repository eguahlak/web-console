using System;
using WebConsoleConnector.Form;
using static WebConsoleConnector.Utilities.Extensions;


namespace TemplateBackend
{
    

    class Program
    {

        private static void TryFormPublish()
        {
            var someLabel = new Label("Location: ");
            var someText = new TextField("We were here", false);
            var someField = new TextField("Change me", true)
            {
                OnUpdate = (c, v) =>
                {
                    Console.WriteLine($">>{c.Id} was updated to {v}");
                    someText.Value = $"We were here with {v}";
                }
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

                }
            };
            form.Publish(8089);
            for (int i = 1; true; i++)
            {
                // Console.WriteLine("Indtast en tekst");
                string text = Console.ReadLine();
                someText.Value = text;
                someLabel.Value = $"{i}. placering";
            }
         }

        static void Main(string[] args)
        {
            TryFormPublish();
        }
    }
}
