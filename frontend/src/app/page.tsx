'use client';

import React, { useEffect, useState } from 'react';
import { Business, Category, Campaign } from '../services/api';
import { businessService, categoryService, campaignService } from '../services/api';
import BusinessCard from '../components/BusinessCard';
import { FaStore, FaTags, FaPercent, FaMapMarkerAlt } from 'react-icons/fa';

export default function HomePage() {
  const [businesses, setBusinesses] = useState<Business[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [campaigns, setCampaigns] = useState<Campaign[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedCategory, setSelectedCategory] = useState<number | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [businessesData, categoriesData, campaignsData] = await Promise.all([
          businessService.getAll(),
          categoryService.getAll(),
          campaignService.getAll()
        ]);
        setBusinesses(businessesData);
        setCategories(categoriesData);
        setCampaigns(campaignsData);
      } catch (error) {
        console.error('Veri yüklenirken hata:', error);
        setError('Veriler yüklenirken bir hata oluştu. Lütfen sayfayı yenileyin.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  // Benzersiz il sayısını hesapla
  const uniqueCities = new Set(businesses.map(business => business.city)).size;

  const filteredBusinesses = businesses.filter(business => {
    const matchesSearch = business.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         business.description.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesCategory = !selectedCategory || business.categoryId === selectedCategory;
    return matchesSearch && matchesCategory;
  });

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-900 flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-indigo-500"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-gray-900 flex items-center justify-center">
        <div className="text-center">
          <h2 className="text-2xl font-semibold text-red-400 mb-4">Hata</h2>
          <p className="text-gray-400">{error}</p>
          <button
            onClick={() => window.location.reload()}
            className="mt-4 px-4 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 transition-colors"
          >
            Sayfayı Yenile
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-900">
      {/* Hero Section */}
      <div className="relative bg-gradient-to-br from-gray-900 via-indigo-900 to-purple-900 py-24">
        <div className="absolute inset-0 bg-[url('/grid.svg')] bg-center [mask-image:linear-gradient(180deg,white,rgba(255,255,255,0))]" />
        <div className="container mx-auto px-4 relative">
          <h1 className="text-5xl md:text-7xl font-bold text-white mb-8 font-display bg-clip-text text-transparent bg-gradient-to-r from-indigo-400 to-purple-400">
            Yerel Ekonomiyi Destekle
          </h1>
          <p className="text-xl text-gray-300 mb-12 max-w-2xl">
            Yerel işletmeleri keşfet, kampanyaları takip et ve ekonomiyi canlandır.
          </p>
          <div className="relative max-w-2xl">
            <input
              type="text"
              placeholder="İşletme veya kampanya ara..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full px-6 py-4 bg-gray-800/50 backdrop-blur-sm border border-gray-700/50 rounded-xl text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent"
            />
          </div>
        </div>
      </div>

      {/* İstatistikler */}
      <div className="container mx-auto px-4 -mt-12">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700/50 hover:border-indigo-500/50 transition-all duration-300">
            <div className="flex items-center space-x-4">
              <div className="w-12 h-12 bg-indigo-900/50 rounded-xl flex items-center justify-center">
                <FaStore className="w-6 h-6 text-indigo-400" />
              </div>
              <div>
                <h3 className="text-2xl font-bold text-white">{businesses.length}</h3>
                <p className="text-gray-400">Aktif İşletme</p>
              </div>
            </div>
          </div>

          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700/50 hover:border-purple-500/50 transition-all duration-300">
            <div className="flex items-center space-x-4">
              <div className="w-12 h-12 bg-purple-900/50 rounded-xl flex items-center justify-center">
                <FaMapMarkerAlt className="w-6 h-6 text-purple-400" />
              </div>
              <div>
                <h3 className="text-2xl font-bold text-white">{uniqueCities}</h3>
                <p className="text-gray-400">İlde Hizmet</p>
              </div>
            </div>
          </div>

          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700/50 hover:border-green-500/50 transition-all duration-300">
            <div className="flex items-center space-x-4">
              <div className="w-12 h-12 bg-green-900/50 rounded-xl flex items-center justify-center">
                <FaTags className="w-6 h-6 text-green-400" />
              </div>
              <div>
                <h3 className="text-2xl font-bold text-white">{categories.length}</h3>
                <p className="text-gray-400">Kategori</p>
              </div>
            </div>
          </div>

          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700/50 hover:border-yellow-500/50 transition-all duration-300">
            <div className="flex items-center space-x-4">
              <div className="w-12 h-12 bg-yellow-900/50 rounded-xl flex items-center justify-center">
                <FaPercent className="w-6 h-6 text-yellow-400" />
              </div>
              <div>
                <h3 className="text-2xl font-bold text-white">{campaigns.length}</h3>
                <p className="text-gray-400">Aktif Kampanya</p>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Kategori Filtreleme */}
      <div className="container mx-auto px-4 py-12">
        <div className="flex flex-wrap gap-4">
          <button
            onClick={() => setSelectedCategory(null)}
            className={`px-6 py-3 rounded-full text-sm font-semibold transition-all duration-300 ${
              selectedCategory === null
                ? 'bg-indigo-600 text-white'
                : 'bg-gray-800 text-gray-400 hover:bg-gray-700'
            }`}
          >
            Tümü
          </button>
          {categories.map((category) => (
            <button
              key={category.categoryId}
              onClick={() => setSelectedCategory(category.categoryId)}
              className={`px-6 py-3 rounded-full text-sm font-semibold transition-all duration-300 ${
                selectedCategory === category.categoryId
                  ? 'bg-indigo-600 text-white'
                  : 'bg-gray-800 text-gray-400 hover:bg-gray-700'
              }`}
            >
              {category.name}
            </button>
          ))}
        </div>
      </div>

      {/* İşletmeler */}
      <div className="container mx-auto px-4 py-12">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {filteredBusinesses.map((business) => (
            <BusinessCard key={business.businessId} business={business} />
          ))}
        </div>

        {filteredBusinesses.length === 0 && (
          <div className="text-center py-12">
            <p className="text-gray-400 text-lg">
              {searchTerm || selectedCategory
                ? 'Arama kriterlerinize uygun işletme bulunamadı.'
                : 'Henüz işletme bulunmuyor.'}
            </p>
          </div>
        )}
      </div>

      {/* Proje Hakkında */}
      <div className="container mx-auto px-4 py-16">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div className="bg-gray-800/50 backdrop-blur-sm rounded-xl p-6 border border-gray-700/50">
            <h3 className="text-lg font-semibold text-white mb-4">Yerel Ekonomi</h3>
            <p className="text-sm text-gray-400 leading-relaxed">
              Yerel işletmeleri destekleyerek topluluk ekonomisini güçlendiriyoruz. 
              Her alışverişinizle yerel ekonomiyi canlandırıyorsunuz.
            </p>
          </div>

          <div className="bg-gray-800/50 backdrop-blur-sm rounded-xl p-6 border border-gray-700/50">
            <h3 className="text-lg font-semibold text-white mb-4">Dijital Dönüşüm</h3>
            <p className="text-sm text-gray-400 leading-relaxed">
              İşletmelerin dijital dünyada daha görünür olmasını sağlıyoruz. 
              Teknoloji ile yerel işletmeleri bir araya getiriyoruz.
            </p>
          </div>

          <div className="bg-gray-800/50 backdrop-blur-sm rounded-xl p-6 border border-gray-700/50">
            <h3 className="text-lg font-semibold text-white mb-4">Topluluk Ruhu</h3>
            <p className="text-sm text-gray-400 leading-relaxed">
              Yerel işletmeler ve müşteriler arasında güçlü bir topluluk oluşturuyoruz. 
              Birlikte daha güçlü bir gelecek inşa ediyoruz.
            </p>
          </div>
        </div>

        <div className="mt-12 text-center">
          <p className="text-sm text-gray-500">
            &copy; {new Date().getFullYear()} Yerel Ekonomi. Tüm hakları saklıdır.
          </p>
        </div>
      </div>
    </div>
  );
} 