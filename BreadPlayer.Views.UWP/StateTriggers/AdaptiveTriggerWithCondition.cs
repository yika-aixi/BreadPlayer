﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace BreadPlayer.StateTriggers
{
    public class AdaptiveTriggerWithCondition : StateTriggerBase, ITriggerValue
    {
        private static double CurrentValue;
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowWidthTrigger"/> class.
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public AdaptiveTriggerWithCondition()
        {
            Window.Current.SizeChanged += MainWindow_SizeChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private double GetCurrentValue()
        {
            return Window.Current.Bounds.Width;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void MainWindow_SizeChanged(object sender, WindowSizeChangedEventArgs windowSizeChangedEventArgs)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion
        /// <summary>
        /// Gets or sets the device family to trigger on.
        /// </summary>
        /// <value>The device family.</value>
        public double MinWindowWidth
        {
            get { return (double)GetValue(MinWindowWidthProperty); }
            set { SetValue(MinWindowWidthProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="DeviceFamily"/> DependencyProperty
        /// </summary>
        public static readonly DependencyProperty MinWindowWidthProperty =
            DependencyProperty.Register("MinWindowWidth", typeof(double), typeof(AdaptiveTriggerWithCondition),
            new PropertyMetadata(null));
        /// <summary>
        /// Gets or sets the device family to trigger on.
        /// </summary>
        /// <value>The device family.</value>
        public bool Condition
        {
            get { return (bool)GetValue(ConditionProperty); }
            set { SetValue(ConditionProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="DeviceFamily"/> DependencyProperty
        /// </summary>
        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register("Condition", typeof(bool), typeof(AdaptiveTriggerWithCondition),
            new PropertyMetadata(false, OnConditionPropertyChanged));

        private static void OnConditionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (AdaptiveTriggerWithCondition)d;
            var val = (bool)e.NewValue;
            if (CurrentValue >= obj.MinWindowWidth && CurrentValue < 900)
                obj.IsActive = val;
            //else if (deviceFamily == "Windows.Desktop")
            //    obj.IsActive = (val == DeviceFamily.Desktop);
            //else if (deviceFamily == "Windows.Team")
            //    obj.IsActive = (val == DeviceFamily.Team);
            //else if (deviceFamily == "Windows.IoT")
            //    obj.IsActive = (val == DeviceFamily.IoT);
            //else if (deviceFamily == "Windows.Holographic")
            //    obj.IsActive = (val == DeviceFamily.Holographic);
            //else if (deviceFamily == "Windows.Xbox")
            //    obj.IsActive = (val == DeviceFamily.Xbox);
            //else
            //    obj.IsActive = (val == DeviceFamily.Unknown);
        }

        #region ITriggerValue

        private bool m_IsActive;

        /// <summary>
        /// Gets a value indicating whether this trigger is active.
        /// </summary>
        /// <value><c>true</c> if this trigger is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get { return m_IsActive; }
            private set
            {
                if (m_IsActive != value)
                {
                    m_IsActive = value;
                    base.SetActive(value);
                    IsActiveChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="IsActive" /> property has changed.
        /// </summary>
        public event EventHandler IsActiveChanged;

        #endregion ITriggerValue
    }
}
