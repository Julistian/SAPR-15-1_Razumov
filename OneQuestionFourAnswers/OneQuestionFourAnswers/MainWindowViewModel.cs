﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;

namespace OneQuestionFourAnswers 
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            //  Пробный вопрос
            LibraryClass.Answer AnswerA = new LibraryClass.Answer("ответ А", false);
            LibraryClass.Answer AnswerB = new LibraryClass.Answer("ответ Б", false);
            LibraryClass.Answer AnswerC = new LibraryClass.Answer("ответ В", true);
            LibraryClass.Answer AnswerD = new LibraryClass.Answer("ответ Г", false);
            List<LibraryClass.Answer> AnswerList = new List<LibraryClass.Answer>();
            AnswerList.Add(AnswerA);
            AnswerList.Add(AnswerB);
            AnswerList.Add(AnswerC);
            AnswerList.Add(AnswerD);
            QuestionPlusAnswers = new LibraryClass.QuestionAnswers("Текст вопроса", AnswerList);
            // /Пробный вопрос   
        }
        public byte AHight { get; set; }
        public byte BHight { get; set; }
        public byte CHight { get; set; }
        public byte DHight { get; set; }
        public string Name { get; set; }

        public int _gamescore { get; set; }
        public int GameScore
        {
            get
            {
                return _gamescore;
            }
            set
            {
                _gamescore = value;
                DoPropertyChanged("GameScore");
            }
        }
        public LibraryClass.QuestionAnswers _questionplusanswers { get; set; }
        public LibraryClass.QuestionAnswers QuestionPlusAnswers
        {
            get
            {
                return _questionplusanswers;
            }
            set
            {
                _questionplusanswers = value;
                DoPropertyChanged("QuestionPlusAnswers");
            }
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
