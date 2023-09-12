using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;
using BlogApi.Services;

namespace BlogApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{

    readonly BlogContext _context;
    public PostsController(BlogContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BlogPost>> Get()
    {
        return _context.BlogPosts;
    }

    [HttpGet("{id}")]
    public ActionResult<BlogPost> Get(int id)
    {
        return _context.BlogPosts.FirstOrDefault(post=> post.BlogPostId==id);
    }

    [HttpPost]
    public void Post([FromBody] BlogPost value)
    {
        _context.BlogPosts.Add(value);
        _context.SaveChanges();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] BlogPost value)
    {
        var post = _context.BlogPosts.FirstOrDefault(p => p.BlogPostId == id);
        if(post==null)
        {
            return;
        }
        post.Title = value.Title;
        post.Summary = value.Summary;
        post.Body = value.Body;
        post.Author = value.Author;
        post.Tags = value.Tags;

        _context.BlogPosts.Update(post);
        _context.SaveChanges();
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var post = _context.BlogPosts.FirstOrDefault(p => p.BlogPostId == id);
        if(post==null)
        {
            return;
        }
        _context.BlogPosts.Remove(post);
        _context.SaveChanges();
    }

    
}
