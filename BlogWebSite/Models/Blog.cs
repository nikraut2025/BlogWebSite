using System.ComponentModel.DataAnnotations;

namespace BlogWebSite.Models;

public class Blog
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Summary { get; set; } = string.Empty;

    public string? FeaturedImage { get; set; }

    [Required]
    public DateTime PublishedDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    [Required]
    public string AuthorId { get; set; } = string.Empty;
    
    public string? AuthorName { get; set; }

    [Required]
    public string Slug { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new();

    public bool IsPublished { get; set; }

    public int ViewCount { get; set; }
} 