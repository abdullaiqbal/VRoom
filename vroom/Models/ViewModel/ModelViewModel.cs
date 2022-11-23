using Microsoft.AspNetCore.Mvc.Rendering;

namespace vroom.Models.ViewModel
{
    public class ModelViewModel
    {
        public Model Model { get; set; }
        public IEnumerable<Make> Makes { get; set; }
        public IEnumerable<SelectListItem> CSelectListItem(IEnumerable<Make> Items)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem Sli = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            listItems.Add(Sli);
            foreach(Make item in Items)
            {
                Sli = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                };
                listItems.Add(Sli);

            }
            return listItems;
        }



        }
    }

