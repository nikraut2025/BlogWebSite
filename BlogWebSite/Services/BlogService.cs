using BlogWebSite.Data;
using BlogWebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebSite.Services;

public class BlogService
{
    private readonly ApplicationDbContext _context;

    public BlogService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Blog>> GetBlogsAsync(int page = 1, int pageSize = 10)
    {
        return await _context.Blogs
            .Where(b => b.IsPublished)
            .OrderByDescending(b => b.PublishedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Blog?> GetBlogBySlugAsync(string slug)
    {
        return await _context.Blogs
            .FirstOrDefaultAsync(b => b.Slug == slug);
    }

    public async Task<Blog> CreateBlogAsync(Blog blog)
    {
        blog.Slug = GenerateSlug(blog.Title);
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
        return blog;
    }

    public async Task<Blog?> UpdateBlogAsync(Blog blog)
    {
        var existingBlog = await _context.Blogs.FindAsync(blog.Id);
        if (existingBlog == null) return null;

        existingBlog.Title = blog.Title;
        existingBlog.Content = blog.Content;
        existingBlog.Summary = blog.Summary;
        existingBlog.FeaturedImage = blog.FeaturedImage;
        existingBlog.LastModifiedDate = DateTime.UtcNow;
        existingBlog.Tags = blog.Tags;
        existingBlog.IsPublished = blog.IsPublished;

        await _context.SaveChangesAsync();
        return existingBlog;
    }

    public async Task<bool> DeleteBlogAsync(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return false;

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return true;
    }

    private string GenerateSlug(string title)
    {
        var slug = title.ToLower()
            .Replace(" ", "-")
            .Replace("&", "and")
            .Replace("?", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("\"", "")
            .Replace(":", "")
            .Replace(";", "")
            .Replace(",", "")
            .Replace(".", "");

        return slug;
    }
}
