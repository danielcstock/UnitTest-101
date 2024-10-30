using Microsoft.EntityFrameworkCore;
using Model.Classes;

public interface IDBContext : IDisposable
{
    DbContext Instance { get; }
}