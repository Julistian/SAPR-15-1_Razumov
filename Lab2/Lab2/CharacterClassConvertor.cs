﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Resources;

namespace Lab2
{
    public class CharacterClassConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value as String)
            {
                case "Саппорт":
                    {
                        return Image.UriSource = new Uri("Images\\Support.jpg", UriKind.RelativeOrAbsolute);
                    }
                case "Файтер":
                    {
                        return Image.UriSource = new Uri("Images\\Warrior.jpg", UriKind.RelativeOrAbsolute);
                    }
                case "Клирик":
                    {
                        return Image.UriSource = new Uri("Images\\Cleric.jpg", UriKind.RelativeOrAbsolute);
                    }
                case "Стрелок":
                    {
                        return Image.UriSource = new Uri("Images\\Shooter.jpg", UriKind.RelativeOrAbsolute);
                    }
                case "Маг":
                    {
                        return Image.UriSource = new Uri("Images\\Mage.jpg", UriKind.RelativeOrAbsolute);
                    }
                default:
                    {
                        return Image.UriSource = new Uri("Images\\All classes.jpg", UriKind.RelativeOrAbsolute);
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        BitmapImage Image = new BitmapImage();
    }
}
