//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SharedModels;
//using System.Collections.Generic;

//namespace PSYCO.Ranpod.Test
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void TestInsertJsonData()
//        {

            
//            //var store = new JsonDataStore("database.json");
//            //var student = new Student { LastName = "Rahivghi", Name = "Ali" };
//            //store.Insert(student, nameof(Student));

//        }

//        [TestMethod]
//        public void TestReadFromJsonData()
//        {
//            var store = new JsonDataStore<TestDb>("database.json");

//            var student = new Student { LastName = "Rahivghi", Name = "Ali2" };
//            var students = store.GetTable<Student>();
//            students.Add(student);
//            store.Database.Students = students;
//            store.ApplyChanges();
            

//        }
//    }
   


//    public class Student
//    {

//        public string Name { get; set; }
//        public string LastName { get; set; }

//    }

//    public class TestDb
//    {

//        public IList<Student>  Students{ get; set; }
//    }
//}
