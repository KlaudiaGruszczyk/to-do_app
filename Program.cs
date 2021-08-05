using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mime;

namespace task_list
{

    class Task
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $" {nameof(Id)}: {Id}, {nameof(Content)}: {Content}";
        }
    }

    class GeneratorId
    {
        private int basicId = 1;
        public int GetNextValue() => basicId++;
    }

    class TaskList
    {
        private static GeneratorId _generatorId = new GeneratorId();

        private List<Task> _task = new List<Task>();
   

        public Task[] Get() => _task.ToArray();
        public Task GetById(int id) => _task.SingleOrDefault(x => x.Id == id);

        public void AddTask(string content)
        {
            _task.Add(new Task
            {
                Id = _generatorId.GetNextValue(),
                Content = content

            });
        }

        public void Delete(int id)
        {
            _task.Remove(GetById(id));
        }

        public void Update(Task task)
        {
            var taskOnList = GetById(task.Id);
            if (taskOnList is null) return;
            taskOnList.Content = Console.ReadLine();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Hello ins your TO DO List app");
            Console.WriteLine("Today is {0}.{1}.{2}", DateTime.Now.Date.Day, DateTime.Now.Date.Month, DateTime.Now.Date.Year);

            Console.WriteLine("Add task - wybierz 1 z klawiatury");
            Console.WriteLine("Modify task - wybierz 2 z klawiatury");
            Console.WriteLine("Show taska - wybierz 3 z klawiatury");
            Console.WriteLine("delete task - wybierz 4 z klawiatury");
            Console.WriteLine("Wyjdź z programu - wybierz 5");
            int menu =0;


            var taskList = new TaskList();
           
            var task = taskList.Get();  

            while (menu != 5)
            {
                Console.WriteLine("Wybierz opcje:");
                var userInput = int.TryParse(Console.ReadLine(), out menu);

                switch (menu)
                {
                    case 1:
                        Console.WriteLine("dodawanie zadania");
                        taskList.AddTask(Console.ReadLine());
                        break;

                    case 2:
                        Console.WriteLine("modyfikowanie zadania");
                        Console.WriteLine("Podaj id zadania");
                        var idm = int.Parse(Console.ReadLine());
                        var taskId = task.Where(x => x.Id == idm).FirstOrDefault();
                        taskList.Update(taskId);
                        break;
                    case 3:
                        Console.WriteLine("wyświetlanie zadan");
                        task = taskList.Get();
                        foreach (var item in task)
                        {
                            Console.WriteLine(item);
                        }

                        break;

                    case 4:
                        Console.WriteLine("usuwanie zadania");
                        Console.WriteLine("Podaj id zadania");
                        var idn = int.Parse(Console.ReadLine());
                        taskList.Delete(idn);
                        break;

                    default:
                        Console.WriteLine("Zły user input");
                        break;

                }
            }
        }
    }
}