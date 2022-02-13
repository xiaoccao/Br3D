using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    // binding이 가능한 double 
    public class BindingDouble : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool enabledNotifyPropertyChanged { get; set; } = true;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if(enabledNotifyPropertyChanged)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            return this.text;
        }

        public BindingDouble(double[] items = null)
        {
            value = 0;
            format = "0.0";
            if (items != null)
            {
                this.items = new List<double>();
                this.items.AddRange(items);
            }
        }


        public BindingDouble(double value, string format)
        {
            this.value = value;
            this.format = format;
        }

        public BindingDouble(double value, string format, double[] items)
        {
            this.value = value;
            this.format = format;
            if (items != null)
            {
                this.items = new List<double>();
                this.items.AddRange(items);
            }
                
        }
        private string format { get; set; }
        private double oldValue { get; set; }  // 값저장 취소를 위해 보관하는 값 
        private double newValue { get; set; } = 0;  // 새로운 값
        private bool backupped { get; set; }
        public List<double> items { get; set; }

        public void Backup()
        {
            oldValue = value;
            backupped = true;
        }

        public void Restore()
        {
            if (!backupped)
                return;
            value = oldValue;
            backupped = false;
        }
        

        public double value
        {
            get { return newValue; }
            set 
            { 
                newValue = value;
                OnPropertyChanged();
            }
        }
        public string ToString(double v)
        {
            return v.ToString(format);
        }
        public string text
        {
            get { return ToString(value); }
            set { this.value = value.ToDouble();}
        }

        public string reverseSignText
        {
            get { return ToString(value * -1); }
            set { this.value = value.ToDouble() * -1; }
        }
        public string absText
        {
            get { return ToString(Math.Abs(value)); }
        }
    }
}
