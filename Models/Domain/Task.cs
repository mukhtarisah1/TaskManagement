using System;

namespace TaskManagementApi.Models.Domain
{
    public class TaskItem
    {
        public Guid Id { get; set;}
        public string Title { get; set;}
        public string Description { get; set;}
        public bool isCompleted { get; set;}
        public DateTime CreatedAt { get; set;}
    }
}