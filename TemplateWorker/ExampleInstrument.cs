using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebConsoleConnector.Form;

namespace TemplateWorker
{

    public class ExampleForm : HttpForm
    {
        public int count = 0;
        public Panel DashBoardPanel { get; }
        public Label CountLabel { get; } = new("Counting: ");
        public Field CountField { get; } = new("0", false) { Width = 10 };
        public Field NoteField { get; } = new("", "Add a note here") { Width = 50 };
        public Button DoItButton { get; } = new("Just Do It!");
        public Button StopButton { get; } = new("*STOP*");

        public ExampleForm() : base("Just an example")
        {
            DoItButton.OnClick = b =>
            {
                count = 0;
                CountField.Value = $"{count:D4}";
            };
            NoteField.OnUpdate = (f, v) =>
            {
                CountLabel.Value = $"{v}:";
            };
            DashBoardPanel = new(true)
            {
                CountLabel, CountField, NoteField, DoItButton
            };
            Add(DashBoardPanel);
            //Add(CountLabel);
            //Add(CountField);
            //Add(NoteField);
            //Add(DoItButton);
            //Add(Newline.Ruler);
            Add(StopButton);
        }
    }

    public class ExampleInstrument
    {
        private ExampleForm form = new ExampleForm();
        private bool isRunning = true;

        public async Task Run(CancellationToken token)
        {
            form.StopButton.OnClick = Stop;
            form.Publish();
            while (isRunning)
            {
                if (token.IsCancellationRequested) break;
                form.count++;
                form.CountField.Value = $"{form.count:D4}";
                await Task.Delay(2_000);
            }
            form.DashBoardPanel.Hidden = true;
            form.Halt("Halted from ExampleInstrument");
        }

        public void Stop(IComponent button)
        {
            isRunning = false;
        }
    }
}
