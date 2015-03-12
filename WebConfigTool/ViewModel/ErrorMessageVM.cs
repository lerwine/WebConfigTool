using System;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class ErrorMessageVM : DependencyObject
    {
        #region Private fields

        private RelayCommand _allowShowErrorCommand = null;
        private RelayCommand _hideErrorCommand = null;
        private RelayCommand _toggleShowErrorCommand = null;
        private RelayCommand _allowShowDetailCommand = null;
        private RelayCommand _hideDetailCommand = null;
        private RelayCommand _toggleHideDetailCommand = null;

        #endregion

        #region Static dependency properties

        #region Behavior properties

        public static readonly DependencyProperty AutoShowErrorProperty = DependencyProperty.Register("AutoShowError", typeof(bool), typeof(ErrorMessageVM),
            new PropertyMetadata(true, ErrorMessageVM.AutoShowError_PropertyChanged));

        public static readonly DependencyProperty DefaultShowDetailProperty = DependencyProperty.Register("DefaultShowDetail", typeof(bool), typeof(ErrorMessageVM),
            new PropertyMetadata(true));

        #endregion

        #region Text properties

        public static readonly DependencyProperty ErrorMessageTextProperty = DependencyProperty.Register("ErrorMessageText", typeof(string), typeof(ErrorMessageVM),
            new PropertyMetadata("", ErrorMessageVM.ErrorMessage_PropertyChanged));

        public static readonly DependencyProperty ErrorDetailTextProperty = DependencyProperty.Register("ErrorDetailText", typeof(string), typeof(ErrorMessageVM),
            new PropertyMetadata("", ErrorMessageVM.ErrorDetail_PropertyChanged));

        #endregion

        #region Display control properties

        public static readonly DependencyProperty HideErrorProperty = DependencyProperty.Register("HideError", typeof(bool), typeof(ErrorMessageVM),
            new PropertyMetadata(false, ErrorMessageVM.HideError_PropertyChanged));

        public static readonly DependencyProperty HideDetailProperty = DependencyProperty.Register("HideDetail", typeof(bool), typeof(ErrorMessageVM),
            new PropertyMetadata(false, ErrorMessageVM.HideDetail_PropertyChanged));

        #endregion

        #region Visibility properties

        public static readonly DependencyProperty HasErrorProperty = DependencyProperty.Register("HasError", typeof(bool), typeof(ErrorMessageVM),
            new PropertyMetadata(false, ErrorMessageVM.HasError_PropertyChanged));

        public static readonly DependencyProperty ErrorIsVisibleProperty = DependencyProperty.Register("ErrorIsVisible", typeof(bool), typeof(ErrorMessageVM),
                new PropertyMetadata(false));

        public static readonly DependencyProperty DetailsAreVisibleProperty = DependencyProperty.Register("DetailsAreVisible", typeof(bool), typeof(ErrorMessageVM),
                new PropertyMetadata(false));

        #endregion

        #endregion

        #region Instance properties

        #region Behavior properties

        /// <summary>
        /// Indicates whether to automatically set ShowError to true when HasError changes from false to true 
        /// </summary>
        public bool AutoShowError
        {
            get { return (bool)(this.GetValue(ErrorMessageVM.AutoShowErrorProperty)); }
            set { this.SetValue(ErrorMessageVM.AutoShowErrorProperty, value); }
        }

        /// <summary>
        /// When HasError changes from false to true, ShowDetail will be set to this value.
        /// </summary>
        public bool DefaultShowDetail
        {
            get { return (bool)(this.GetValue(ErrorMessageVM.DefaultShowDetailProperty)); }
            set { this.SetValue(ErrorMessageVM.DefaultShowDetailProperty, value); }
        }

        #endregion

        #region Text properties

        /// <summary>
        /// Brief message text for error
        /// </summary>
        public string ErrorMessageText
        {
            get { return (string)(this.GetValue(ErrorMessageVM.ErrorMessageTextProperty)); }
            set { this.SetValue(ErrorMessageVM.ErrorMessageTextProperty, (value == null) ? "" : value.Trim()); }
        }

        /// <summary>
        /// Detailed error description
        /// </summary>
        public string ErrorDetailText
        {
            get { return (string)(this.GetValue(ErrorMessageVM.ErrorDetailTextProperty)); }
            set { this.SetValue(ErrorMessageVM.ErrorDetailTextProperty, (value == null) ? "" : value.Trim()); }
        }

        #endregion

        #region Display control properties

        /// <summary>
        /// Indicates whether the entire error view should be hidden
        /// </summary>
        public bool HideError
        {
            get { return (bool)(this.GetValue(ErrorMessageVM.HideErrorProperty)); }
            set { this.SetValue(ErrorMessageVM.HideErrorProperty, value); }
        }

        /// <summary>
        /// Indicates whether the detailed error description should be hidden
        /// </summary>
        public bool HideDetail
        {
            get { return (bool)(this.GetValue(ErrorMessageVM.HideDetailProperty)); }
            set { this.SetValue(ErrorMessageVM.HideDetailProperty, value); }
        }

        #endregion

        #region Command properties

        /// <summary>
        /// Command which permits the error to be shown
        /// </summary>
        public RelayCommand AllowShowErrorCommand
        {
            get
            {
                if (this._allowShowErrorCommand == null)
                    this._allowShowErrorCommand = new RelayCommand((object o) => this.HideError = false);

                return this._allowShowErrorCommand;
            }
        }

        /// <summary>
        /// Command which hides the error view
        /// </summary>
        public RelayCommand HideErrorCommand
        {
            get
            {
                if (this._hideErrorCommand == null)
                    this._hideErrorCommand = new RelayCommand((object o) => this.HideError = true);

                return this._hideErrorCommand;
            }
        }

        /// <summary>
        /// Command which toggles the error view's visibility
        /// </summary>
        public RelayCommand ToggleHideErrorCommand
        {
            get
            {
                if (this._toggleShowErrorCommand == null)
                    this._toggleShowErrorCommand = new RelayCommand((object o) => this.HideError = !this.HideError);

                return this._toggleShowErrorCommand;
            }
        }

        /// <summary>
        /// Command which permits the error detail to be shown
        /// </summary>
        public RelayCommand AllowShowDetailCommand
        {
            get
            {
                if (this._allowShowDetailCommand == null)
                    this._allowShowDetailCommand = new RelayCommand((object o) => this.HideDetail = false);

                return this._allowShowDetailCommand;
            }
        }

        /// <summary>
        /// Command which hides the error detail
        /// </summary>
        public RelayCommand HideDetailCommand
        {
            get
            {
                if (this._hideDetailCommand == null)
                    this._hideDetailCommand = new RelayCommand((object o) => this.HideDetail = true);

                return this._hideDetailCommand;
            }
        }

        /// <summary>
        /// Command which toggles the error detail visibility
        /// </summary>
        public RelayCommand ToggleHideDetailCommand
        {
            get
            {
                if (this._toggleHideDetailCommand == null)
                    this._toggleHideDetailCommand = new RelayCommand((object o) => this.HideDetail = !this.HideDetail);

                return this._toggleHideDetailCommand;
            }
        }

        #endregion

        #region Visibility properties

        /// <summary>
        /// Indicates hether the object contains any error text
        /// </summary>
        public bool HasError
        {
            get { return (bool)(this.GetValue(ErrorMessageVM.HasErrorProperty)); }
            private set { this.SetValue(ErrorMessageVM.HasErrorProperty, value); }
        }

        /// <summary>
        /// Indicates whether there is an error and it is not to be hidden.
        /// </summary>
        public bool ErrorIsVisible
        {
            get { return (bool)(this.GetValue(ErrorMessageVM.ErrorIsVisibleProperty)); }
            private set { this.SetValue(ErrorMessageVM.ErrorIsVisibleProperty, value); }
        }

        /// <summary>
        /// Indicates whether there are error details and they are not to be hidden.
        /// </summary>
        public bool DetailsAreVisible
        {
            get { return (bool)(this.GetValue(ErrorMessageVM.DetailsAreVisibleProperty)); }
            private set { this.SetValue(ErrorMessageVM.DetailsAreVisibleProperty, value); }
        }

        #endregion

        #endregion

        public ErrorMessageVM() : base() { }

        public ErrorMessageVM(string message)
            : base()
        {
            this.ErrorMessageText = message;
        }

        public ErrorMessageVM(string message, string details)
            : this(message)
        {
            this.ErrorDetailText = details;
        }

        #region Static property change event handlers

        #region Behavior change handlers

        private static void AutoShowError_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ErrorMessageVM vm = d as ErrorMessageVM;

            if (!vm.HasError || !vm.AutoShowError || vm.ErrorIsVisible)
                return;

            vm.HideError = false;
            vm.DetailsAreVisible = (vm.ErrorDetailText.Length > 0 && vm.DefaultShowDetail);
        }

        #endregion

        #region Text change handlers

        private static void ErrorMessage_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ErrorMessageVM vm = d as ErrorMessageVM;

            vm.HasError = (vm.ErrorMessageText.Length > 0 || vm.ErrorDetailText.Length > 0);
        }

        private static void ErrorDetail_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ErrorMessageVM vm = d as ErrorMessageVM;

            bool hasError = (vm.ErrorMessageText.Length > 0 || vm.ErrorDetailText.Length > 0);

            if (vm.HasError != hasError)
                vm.HasError = hasError;
            else if (vm.ErrorDetailText.Length == 0)
                vm.DetailsAreVisible = false;
            else if (vm.DefaultShowDetail == vm.HideDetail)
                vm.HideDetail = !vm.DefaultShowDetail;
            else
                vm.DetailsAreVisible = !vm.HideDetail;
        }

        #endregion

        #region Display change handlers

        private static void HideError_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ErrorMessageVM vm = d as ErrorMessageVM;

            vm.ErrorIsVisible = vm.HasError && !vm.HideError;
        }

        private static void HideDetail_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ErrorMessageVM vm = d as ErrorMessageVM;

            vm.DetailsAreVisible = (vm.ErrorDetailText.Length > 0 && !vm.HideDetail);
        }

        #endregion

        #region Visiblity handlers

        private static void HasError_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ErrorMessageVM vm = d as ErrorMessageVM;

            if (!vm.HasError)
            {
                vm.ErrorIsVisible = false;
                return;
            }

            if (!vm.AutoShowError)
                return;

            vm.ErrorIsVisible = true;

            if (vm.HideDetail == vm.DefaultShowDetail)
                vm.HideDetail = !vm.DefaultShowDetail;
            else
                vm.DetailsAreVisible = (vm.ErrorDetailText.Length > 0 && !vm.HideDetail);
        }

        #endregion

        #endregion

        #region Instance methods

        public void ClearError()
        {
            this.SetError(null as string);
        }

        public void SetError(string message)
        {
            this.SetError(message, null as string);
        }

        public void SetError(Exception error)
        {
            this.SetError(null, error);
        }

        public void SetError(string message, string detail)
        {
            this.ErrorMessageText = message;
            this.ErrorDetailText = detail;
        }

        public void SetError(string message, Exception error)
        {
            if (error == null)
                this.SetError(message, null as string);
            else
                this.SetError(error.Message, error.ToString());
        }

        #endregion
    }
}
