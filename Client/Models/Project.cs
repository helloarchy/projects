﻿using System;

namespace Client.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}