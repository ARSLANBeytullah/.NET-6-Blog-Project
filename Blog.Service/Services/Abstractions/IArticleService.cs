﻿using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticleAsync();  //Code Refactoring yapılacak(Article modeli yerine Dto dediğimiz Data Transfer Object oluşturulacaktır.)
    }
}