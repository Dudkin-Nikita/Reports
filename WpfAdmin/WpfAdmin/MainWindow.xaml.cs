using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string connectionString = "Data Source=DESKTOP-UUPP9C7\\SQLEXPRESS; Database=Reports; Trusted_Connection=True; Encrypt=False";
        ObservableCollection<User> users = new();
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string sqlExpression = "SELECT Id, Name, LastSystemEnter, UserType, UserName from AspNetUsers";
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(PopulateFromRecord(reader));
                }
                reader.Close();
            }
            DataContext = users;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UserWindow UserWindow = new UserWindow(new User());
            if (UserWindow.ShowDialog() == true)
            {
                User user = UserWindow.User;
                user.Id = Guid.NewGuid().ToString();
                user.PasswordHash = new PasswordHasher<User>().HashPassword(null, user.PasswordHash);
                string sqlExpression = "INSERT INTO AspNetUsers (Id, Name, LastSystemEnter, UserType, UserName, NormalizedUserName, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) VALUES (@Id, @Name, @LastSystemEnter, @UserType, @UserName, @NormalizedUserName, @AccessFailedCount, @PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEnabled, @AccessFailedCount)";
                using (SqlConnection connection = new(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        PopulateParameters(command, user);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
                users.Add(user);
            }
        }

        private void Block_Click(object sender, RoutedEventArgs e)
        {
            User? user = usersList.SelectedItem as User;
            if (user is null) return;
            user.UserType = UserTypes.Blocked;
            string sqlExpression = "UPDATE AspNetUsers SET UserType=@UserType WHERE Id = @Id";
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                PopulateParameters(command, user);
                command.ExecuteNonQuery();
            }
            usersList.Items.Refresh();
        }

        private void UnBlock_Click(object sender, RoutedEventArgs e)
        {
            User? user = usersList.SelectedItem as User;
            if (user is null) return;
            user.UserType = UserTypes.Active;
            string sqlExpression = "UPDATE AspNetUsers SET UserType=@UserType WHERE Id = @Id";
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                PopulateParameters(command, user);
                command.ExecuteNonQuery();
            }
            usersList.Items.Refresh();
        }

        private static User PopulateFromRecord(IDataRecord record)
        {
            User user = new()
            {
                Id = record.GetString(0),
                Name = record.GetString(1),
                LastSystemEnter = record.GetDateTime(2),
                UserType = (UserTypes)record.GetInt32(3),
                UserName = record.GetString(4)
            };
            return user;
        }

        private static void PopulateParameters(SqlCommand cmd, User user)
        {
            cmd.Parameters.Clear();

            cmd.Parameters.Add("@Id", SqlDbType.NVarChar, 450);
            cmd.Parameters["@Id"].Value = user.Id;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = user.Name;

            cmd.Parameters.Add("@LastSystemEnter", SqlDbType.DateTime2, 7);
            cmd.Parameters["@LastSystemEnter"].Value = user.LastSystemEnter;

            cmd.Parameters.Add("@UserType", SqlDbType.Int);
            cmd.Parameters["@UserType"].Value = user.UserType;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 256);
            cmd.Parameters["@UserName"].Value = user.UserName;

            cmd.Parameters.Add("@NormalizedUserName", SqlDbType.NVarChar, 256);
            cmd.Parameters["@NormalizedUserName"].Value = user.UserName.ToUpper();

            //cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 256);
            //cmd.Parameters["@Email"].Value = user.Email;

            //cmd.Parameters.Add("@NormalizedEmail", SqlDbType.NVarChar, 256);
            //cmd.Parameters["@NormalizedEmail"].Value = null;

            cmd.Parameters.Add("@EmailConfirmed", SqlDbType.Bit);
            cmd.Parameters["@EmailConfirmed"].Value = user.EmailConfirmed;

            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar);
            cmd.Parameters["@PasswordHash"].Value = user.PasswordHash;

            cmd.Parameters.Add("@SecurityStamp", SqlDbType.NVarChar);
            cmd.Parameters["@SecurityStamp"].Value = "";

            cmd.Parameters.Add("@ConcurrencyStamp", SqlDbType.NVarChar);
            cmd.Parameters["@ConcurrencyStamp"].Value = Guid.NewGuid().ToString();

            //cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar);
            //cmd.Parameters["@PhoneNumber"].Value = user.PhoneNumber;

            cmd.Parameters.Add("@PhoneNumberConfirmed", SqlDbType.Bit);
            cmd.Parameters["@PhoneNumberConfirmed"].Value = user.PhoneNumberConfirmed;

            cmd.Parameters.Add("@TwoFactorEnabled", SqlDbType.Bit);
            cmd.Parameters["@TwoFactorEnabled"].Value = user.TwoFactorEnabled;

            //cmd.Parameters.Add("@LockoutEnd", SqlDbType.DateTimeOffset);
            //cmd.Parameters["@LockoutEnd"].Value = null;

            cmd.Parameters.Add("@LockoutEnabled", SqlDbType.Bit);
            cmd.Parameters["@LockoutEnabled"].Value = user.LockoutEnabled;

            cmd.Parameters.Add("@AccessFailedCount", SqlDbType.Int);
            cmd.Parameters["@AccessFailedCount"].Value = user.AccessFailedCount;
        }
    }
}