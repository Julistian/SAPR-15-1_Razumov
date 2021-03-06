﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab2
{
    class CharacterCreatorViewModel : INotifyPropertyChanged
    {
        public ICommand Cancel { get; set; }
        public ICommand Save { get; set; }
        public byte Force { get; set; }
        public byte Agility { get; set; }
        public byte Intelligence { get; set; }
        public byte Luck { get; set; }
        private byte _freePoints { get; set; }
        public byte FreePoints
        {
            get
            {
                return _freePoints;
            }
            set
            {
                _freePoints = value;
                DoPropertyChanged("FreePoints");
            }
        }
        public string _nickname { get; set; }
        public string Nickname
        {
            get
            {
                return _nickname;
            }
            set
            {
                _nickname = value;
                DoPropertyChanged("Nickname");
            }
        }
        public List<string> CharacterClass { get; set; }
        private string _selectedCharacterClass { get; set; }
        public string SelectedCharacterClass
        {
            get
            {
                return _selectedCharacterClass;
            }
            set
            {
                _selectedCharacterClass = value;
                DoPropertyChanged("SelectedCharacterClass");
            }
        }
        public CharacterCreatorViewModel()
        {
            CharacterClass = new List<string>()
            {
                "Саппорт",
                "Файтер",
                "Клирик",
                "Стрелок",
                "Маг"
            };
            FreePoints = 10;
            Force = 5;
            Agility = 5;
            Intelligence = 5;
            Luck = 5;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
