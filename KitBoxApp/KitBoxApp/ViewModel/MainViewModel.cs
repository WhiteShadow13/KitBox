﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitBox.Core;
using KitBox.Core.Model;
using KitBox.Core.Constraint;

namespace KitBox.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes

        private Box m_box;

        #endregion

        #region Properties
        public Cupboard Cupboard { get; set; }

        public Box SelectedBox
        {
            get { return m_box; }
            set { m_box = value; Notify("SelectedBox"); }
        }

        #endregion

        #region Property changed members

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        #region Icommand
        public ICommand AddBoxCommand
        {
            get => new CommandHandler((x) =>
            {
                try
                {
                    Cupboard.AddBox();
                }
                catch (WarningException e)
                {
                    MessageBox.Show(e.Message);
                }
            }, true);
        }
        public ICommand DeleteBoxCommand
        {
            get => new CommandHandler((x) => { if (Cupboard.Boxes.Count > 1) Cupboard.RemoveBox(SelectedBox); }, true);
        }
        public ICommand ValidateCommand
        {
            get => new CommandHandler((x) =>
            {
                OrderRecap w = new OrderRecap();
                OrderRecapViewModel orderConfirmVM = new OrderRecapViewModel(Cupboard);
                w.DataContext = orderConfirmVM;
                w.ShowDialog();
            }, true);
        }
        public ICommand ResetCommand
        {
            get => new CommandHandler((x) =>
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    InitCupboard();
                }
            }, true);
        }

        #endregion

        #region Constroctor / Init
        public MainViewModel()
        {
            InitCupboard();
        }

        private void InitCupboard()
        {
            Cupboard = new Cupboard();
            Cupboard.CupboardConstraint.Widths = ConstraintBuilder.BuildWidthsList();

            //ajout connect DB
            Cupboard.CupboardConstraint.MaxHeight = 150;
            Cupboard.Width = Cupboard.CupboardConstraint.Widths[0];
            Cupboard.AddBox();
            SelectedBox = Cupboard.Boxes[0];
            Notify("Cupboard");
        }
        #endregion

        
    }
}
