﻿using Data.Entities;

namespace Data.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task BanUser();
    }
}
