using HastaneMVC.Data;
using HastaneMVC.DTOs;
using HastaneMVC.Models;
using HastaneMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HastaneMVC.Services
{
    public class DoktorService : BaseService
    {

        // program cs de              builder.Services.AddScoped<DoktorService>(); yazılmalı 


        public DoktorService(HastaneContext context) : base(context)
        {


        }

        public ICollection<DoktorModel> indexgetir(string ara)
        {


            ICollection<DoktorModel> doktorlistesi;

            if (ara != null)
            {
                doktorlistesi = _context.Doktorlar.Where(d => d.Brans.BransAdi == ara.ToUpper() && d.IsDeleted == false).Select(d => new DoktorModel
                {
                    Id = d.Id,
                    AdSoyad = d.AdSoyad,
                    BransId = d.BransId,
                    Brans = d.Brans

                }).OrderBy(i => i.AdSoyad).ToList();
            }
            else
            {
                doktorlistesi = _context.Doktorlar.Where(d => d.IsDeleted == false).Select(d => new DoktorModel
                {
                    Id = d.Id,
                    AdSoyad = d.AdSoyad,
                    BransId = d.BransId,
                    Brans = d.Brans
                }).ToList();
            }
            return doktorlistesi;



        }

        public DoktorEkleVM EkleGet()
        {



            var model = new DoktorEkleVM
            {
                BransListesi = branslarigetir(),
                YeniDoktor = new DoktorModelDTO() // Ekrana boş bir doktor form alanı gönde
            };

            return model;

        }



        public DoktorModel EklePost(DoktorEkleVM gelenKutu)
        {


            var entity = new DoktorModel
            {
                AdSoyad = gelenKutu.YeniDoktor.AdSoyad.ToUpper(),
                BransId = gelenKutu.YeniDoktor.BransId
            };

            return entity;

        }

        public DoktorEkleVM GuncelleGet(int id)
        {
            var guncellemeDoktor = _context.Doktorlar.Find(id);

            var model = new DoktorEkleVM
            {
                YeniDoktor = new DoktorModelDTO
                {
                    Id = guncellemeDoktor.Id,
                    AdSoyad = guncellemeDoktor.AdSoyad,
                    BransId = guncellemeDoktor.BransId
                },
                BransListesi = branslarigetir()
            };

            return model; 


             
        }

        public void GuncellePost(DoktorEkleVM model)
        {
            var eskiDoktor = _context.Doktorlar.Find(model.YeniDoktor.Id);
            eskiDoktor.AdSoyad = model.YeniDoktor.AdSoyad.ToUpper();
            eskiDoktor.BransId = model.YeniDoktor.BransId;
            _context.SaveChanges();


        }






    }
}

