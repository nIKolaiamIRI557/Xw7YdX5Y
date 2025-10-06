// 代码生成时间: 2025-10-07 02:06:24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

// 定义数据模型
public class Certificate
{
    public int Id { get; set; } // 证书的唯一标识符
    public string Name { get; set; } // 证书名称
    public string Description { get; set; } // 证书描述
    public DateTime IssueDate { get; set; } // 发行日期
    public DateTime ExpiryDate { get; set; } // 过期日期
}

// 定义数据库上下文
public class CertificateDbContext : DbContext
{
    public DbSet<Certificate> Certificates { get; set; } // 证书集合

    public CertificateDbContext(DbContextOptions<CertificateDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Certificate>()
            .HasKey(c => c.Id); // 设置主键
        modelBuilder.Entity<Certificate>()
            .Property(c => c.Name)
            .IsRequired(); // 设置必填字段
    }
}

// 证书管理系统服务
public class CertificateManagementService
{
    private readonly CertificateDbContext _context;

    public CertificateManagementService(CertificateDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 添加证书
    public Certificate AddCertificate(Certificate certificate)
    {
        if (certificate == null) throw new ArgumentNullException(nameof(certificate));
        _context.Certificates.Add(certificate);
        _context.SaveChanges();
        return certificate;
    }

    // 获取所有证书
    public IEnumerable<Certificate> GetAllCertificates()
    {
        return _context.Certificates.ToList();
    }

    // 根据ID获取证书
    public Certificate GetCertificateById(int id)
    {
        return _context.Certificates.FirstOrDefault(c => c.Id == id);
    }

    // 更新证书
    public Certificate UpdateCertificate(Certificate certificate)
    {
        if (certificate == null) throw new ArgumentNullException(nameof(certificate));
        _context.Certificates.Update(certificate);
        _context.SaveChanges();
        return certificate;
    }

    // 删除证书
    public void DeleteCertificate(int id)
    {
        var certificate = _context.Certificates.FirstOrDefault(c => c.Id == id);
        if (certificate != null)
        {
            _context.Certificates.Remove(certificate);
            _context.SaveChanges();
        }
    }
}
