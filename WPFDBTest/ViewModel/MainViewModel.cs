using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFDBTest.Model;

namespace WPFDBTest.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        Student _stu = new Student();

        public string NAME
        {
            get
            {
                return _stu.NAME;
            }
            set
            {
                _stu.NAME = value;
                OnPropertyChanged("NAME");
            }
        }

        public string AGE
        {
            get
            {
                return _stu.AGE;
            }
            set
            {
                _stu.AGE = value;
                OnPropertyChanged("AGE");
            }
        }

        public string GRADE
        {
            get
            {
                return _stu.GRADE;
            }
            set
            {
                _stu.GRADE = value;
                OnPropertyChanged("GRADE");
            }
        }

        public string SCORE
        {
            get
            {
                return _stu.SCORE;
            }
            set
            {
                _stu.SCORE = value;
                OnPropertyChanged("SCORE");
            }
        }

        ObservableCollection<Student> _sampleDatas = null;
        public ObservableCollection<Student> SampleDatas
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<Student>();
                }
                return _sampleDatas;
            }
            set
            {
                _sampleDatas = value;
            }
        }

        //PropertyChaneged 이벤트 선언 및 이벤트 핸들러
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Connect Command 선언
        /// </summary>
        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return (this.connectCommand) ?? (this.connectCommand = new DelegateCommand(Connect));
            }
        }

        private ICommand selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                return (this.selectCommand) ?? (this.selectCommand = new DelegateCommand(DataSearch));
            }
        }

        private ICommand loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                return (this.loadedCommand) ?? (this.loadedCommand = new DelegateCommand(LoadEvent));
            }
        }

        private void LoadEvent()
        {
            //Connect to DB
            if (SqlDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error");
                return;
            }
            else
            {
                string msg = $"Success to Connect to Database";
                MessageBox.Show(msg, "Inform");
            }
        }

        private void DataSearch()
        {
            
            DataSet ds = new DataSet();

            string query = @"
SELECT '범범조조' AS NAME, '28' AS AGE, '4' AS GRADE, '90' AS SCORE
UNION ALL
SELECT '안정환' AS NAME, '26' AS AGE, '4' AS GRADE, '80' AS SCORE
UNION ALL
SELECT '김성주' AS NAME, '36' AS AGE, '4' AS GRADE, '87' AS SCORE
UNION ALL
SELECT '정형돈' AS NAME, '46' AS AGE, '3' AS GRADE, '40' AS SCORE
UNION ALL
SELECT '아이유' AS NAME, '26' AS AGE, '2' AS GRADE, '62' AS SCORE
UNION ALL
SELECT '이기자' AS NAME, '22' AS AGE, '1' AS GRADE, '100' AS SCORE
";

            SqlDBManager.Instance.ExecuteDsQuery(ds, query);

            for(int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                 Student obj = new Student();
                 obj.NAME = ds.Tables[0].Rows[idx]["NAME"].ToString();
                 obj.AGE = ds.Tables[0].Rows[idx]["AGE"].ToString();
                 obj.GRADE = ds.Tables[0].Rows[idx]["GRADE"].ToString();
                 obj.SCORE = ds.Tables[0].Rows[idx]["SCORE"].ToString();

                SampleDatas.Add(obj);
            }
        }

        /// <summary>
        /// DB Connect
        /// </summary>
        private void Connect()
        {
            //Connect to DB
            if (SqlDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error");
                return;
            }
            else
            {
                string msg = $"Success to Connect to Database";
                MessageBox.Show(msg, "Inform");
            }
        }
    }
}
