﻿using Dev69Restaurant.DAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dev69Restaurant.GUI.InfoUser
{
    public partial class InfoUserForm : Form
    {
        private DTO.Entities.User _currentUser;
        private UserService _userService;
        public InfoUserForm()
        {
            InitializeComponent();
        }

        public InfoUserForm(DTO.Entities.User user)
        {
            InitializeComponent();
            _currentUser = user;
            _userService = new UserService();
            GetInfoUser(user);
        }

        string appPath = "";
        string iName = "";   // <---
        string filepath = "";   // <---

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            ValidateForm(appPath + iName);
            SaveImage();
        }

        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFile = new OpenFileDialog();
            opFile.Title = "Select a Image";
            opFile.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

            appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\ProImages\"; // <---
            if (Directory.Exists(appPath) == false)                                              // <---
            {                                                                                    // <---
                Directory.CreateDirectory(appPath);                                              // <---
            }                                                                                    // <---

            if (opFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    iName = opFile.SafeFileName;   // <---
                    filepath = opFile.FileName;    // <---
                    picAvatar.Image = new Bitmap(opFile.OpenFile());
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                }
            }
            else
            {
                opFile.Dispose();
            }
        }

        private void SaveImage()
        {
            try
            {
                if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(appPath) || string.IsNullOrEmpty(iName))
                {
                    return;
                }
                else
                {
                    if (File.Exists(appPath + iName))
                    {
                        return;
                    }
                    else
                    {
                        File.Copy(filepath, appPath + iName);
                    }
                }

            }
            catch(Exception exp)
            {
                MessageBox.Show("Unable to open file " + exp.Message);
            }
        }

        private void ValidateForm(string avatar)
        {
            DTO.Entities.User user = new DTO.Entities.User();
            user.Username = txtUsername.Text;
            user.DisplayName = txtDisplayName.Text;

            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                user.FullName = txtFullName.Text;
            }

            if (!string.IsNullOrEmpty(avatar))
            {
                user.Avatar = avatar;
            }
            else
            {
                user.Avatar = _currentUser.Avatar;
            }

            user.Address = txtAddress.Text;
            user.Phone = txtPhone.Text;
            user.BirthDay = dtpBirthDay.Value.Date;

            _userService.UpdateInfo(user);
            MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GetInfoUser(DTO.Entities.User user)
        {
            var us = _userService.GetInfo(user);
            txtUsername.Text = us.Username;
            txtFullName.Text = us.FullName;
            txtDisplayName.Text = us.DisplayName;
            txtPhone.Text = us.Phone;
            if (us.Avatar != null)
            {
                try
                {
                    picAvatar.Image = Image.FromFile(us.Avatar);
                }
                catch
                {
                    picAvatar.Image = null;
                }
            }
            txtAddress.Text = us.Address;
            dtpBirthDay.Value = us.BirthDay.Value;
        }
    }
}