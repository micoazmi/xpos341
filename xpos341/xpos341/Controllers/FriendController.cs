using Microsoft.AspNetCore.Mvc;
using xpos341.Models;

namespace xpos341.Controllers
{
    public class FriendController : Controller
    {
        private static List<Friend> friends = new List<Friend>()
        {
            new Friend(){Id=1,Name="Sheva",Address="Palembang"},
            new Friend(){Id=2,Name="Fadil",Address="Tangerang"},
            new Friend(){Id=3,Name="Vendra",Address="Pemalang"},
        };
        public IActionResult Index()
        {
            //List<Friend> friends = new List<Friend>()
            //{
            //    new Friend(){Id=1,Name="Sheva",Address="Palembang"},
            //    new Friend(){Id=2,Name="Fadil",Address="Tangerang"},
            //    new Friend(){Id=3,Name="Vendra",Address="Pemalang"},
            //};

            ViewBag.listFriend = friends;

            return View();
            //return View(friends);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Friend friend)
        {
            friends.Add(friend);
            return RedirectToAction("Index");
            //return RedirectToAction("Index", "Friend");
            //return Redirect("Index");
        }

        public IActionResult Edit(int id)
        {
            //Friend friend = friends.Where(friend => friend.Id == id).FirstOrDefault();
            Friend friend = friends.Find(x => x.Id == id);

            return View(friend);
        }

        [HttpPost]
        public IActionResult Edit(Friend data)
        {
            Friend friend = friends.Find(a => a.Id == data.Id);
            int index = friends.IndexOf(friend);

            if (index != -1)
            {
                friends[index].Name = data.Name;
                friends[index].Address = data.Address;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            Friend friend = friends.Find(x => x.Id == id);

            return View(friend);
        }

        public IActionResult Delete(int id)
        {
            Friend friend = friends.Find(x => x.Id == id);

            return View(friend);
        }

        [HttpPost]
        public IActionResult Delete(Friend data)
        {
			Friend friend = friends.Find(x => x.Id == data.Id);
            friends.Remove(friend);

            return RedirectToAction("Index");

		}

    }
}
