// 代码生成时间: 2025-10-02 18:28:49
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Migrations;

// Define the member entity class
public class Member
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime JoinDate { get; set; }
}

// Define the database context for Entity Framework
public class MembershipContext : DbContext
{
    public MembershipContext() : base("name=MembershipContext")
    {
    }

    public DbSet<Member> Members { get; set; }
}

// Membership Management class
public class MembershipManager
{
    private MembershipContext context;

    public MembershipManager(MembershipContext context)
    {
        this.context = context;
    }

    // Add a new member
    public void AddMember(Member member)
    {
        if (member == null)
            throw new ArgumentNullException("member");

        try
        {
            context.Members.Add(member);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the operation
            Console.WriteLine("An error occurred while adding a member: " + ex.Message);
        }
    }

    // Update an existing member
    public void UpdateMember(Member member)
    {
        if (member == null)
            throw new ArgumentNullException("member");

        try
        {
            context.Entry(member).State = EntityState.Modified;
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the update operation
            Console.WriteLine("An error occurred while updating a member: " + ex.Message);
        }
    }

    // Delete a member
    public void DeleteMember(int memberId)
    {
        try
        {
            var member = context.Members.Find(memberId);
            if (member != null)
            {
                context.Members.Remove(member);
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the deletion operation
            Console.WriteLine("An error occurred while deleting a member: " + ex.Message);
        }
    }

    // Get all members
    public List<Member> GetAllMembers()
    {
        return context.Members.ToList();
    }

    // Get a member by ID
    public Member GetMemberById(int memberId)
    {
        return context.Members.Find(memberId);
    }
}

// Program entry point
class Program
{
    static void Main(string[] args)
    {
        var context = new MembershipContext();
        var membershipManager = new MembershipManager(context);

        try
        {
            // Example usage: Add a new member
            Member newMember = new Member
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                JoinDate = DateTime.Now
            };
            membershipManager.AddMember(newMember);

            // Example usage: Get all members
            List<Member> members = membershipManager.GetAllMembers();
            foreach (var member in members)
            {
                Console.WriteLine("Member ID: {0}, Name: {1}, Email: {2}, Join Date: {3}", member.MemberId, member.Name, member.Email, member.JoinDate);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}