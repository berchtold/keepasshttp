﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KeePassLib;

namespace KeePassHttp
{
    public partial class AccessControlForm : Form
    {
        public AccessControlForm()
        {
            InitializeComponent();
        }

        private void AllowButton_Click(object sender, EventArgs e)
        {
            Allowed = true;
            Close();
        }

        private void DenyButton_Click(object sender, EventArgs e)
        {
            Denied = true;
            Close();
        }

        public bool Allowed = false;
        public bool Denied = false;

        public bool Remember
        {
            get { return RememberCheck.Checked; }
        }

        public List<PwEntry> Entries
        {
            set
            {
                EntriesBox.SelectionMode = SelectionMode.None;
                Count = value.Count;
                SetLabel();
                foreach (var e in value)
                {
                    var title = e.Strings.Get(PwDefs.TitleField).ReadString();
                    var username = Plugin.GetUserPass(e)[0];

                    EntriesBox.Items.Add(title + " - " + username);
                }
            }
        }

        public string Host
        {
            set
            {
                _Host = value;
                SetLabel();
            }
        }

        private void SetLabel()
        {
            if (_Host == null)
                return;
            ConfirmTextLabel.Text = String.Format(Message, _Host, Count == 1 ? "item" : "items");
        }

        private int Count = 0;
        private const string Message = "{0} has requested access to passwords for the above {1}. " +
            "Please select whether you want to allow access.";
        private string _Host = null;
        internal KeePassHttpExt Plugin = null;
    }
}
