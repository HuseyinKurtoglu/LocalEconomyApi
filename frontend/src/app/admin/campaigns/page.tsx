'use client';

import { useState, useEffect } from 'react';
import { campaignService, businessService, categoryService, type Campaign, type Business, type Category } from '@/services/api';
import { toast } from 'react-hot-toast';

export default function CampaignManagement() {
  const [campaigns, setCampaigns] = useState<Campaign[]>([]);
  const [businesses, setBusinesses] = useState<Business[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [newCampaign, setNewCampaign] = useState({
    title: '',
    description: '',
    discountRate: 0,
    startDate: '',
    endDate: '',
    businessId: '',
    categoryId: ''
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    setLoading(true);
    try {
      const [campaignsData, businessesData, categoriesData] = await Promise.all([
        campaignService.getAll(),
        businessService.getAll(),
        categoryService.getAll()
      ]);
      setCampaigns(campaignsData.filter((c: Campaign) => !c.isDeleted));
      setBusinesses(businessesData);
      setCategories(categoriesData);
    } catch (error: any) {
      console.error('Veri yüklenirken hata:', error);
      toast.error(error.message || 'Veriler yüklenirken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setNewCampaign(prev => ({
      ...prev,
      [name]: ['businessId', 'categoryId'].includes(name) ? 
        (value === '' ? '' : parseInt(value)) :
        name === 'discountRate' ? 
          (value === '' ? 0 : Math.min(Math.max(parseInt(value) || 0, 0), 100)) :
          value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await campaignService.create(newCampaign);
      toast.success('Kampanya başarıyla eklendi');
      setNewCampaign({
        title: '',
        description: '',
        discountRate: 0,
        startDate: '',
        endDate: '',
        businessId: '',
        categoryId: ''
      });
      loadData();
    } catch (error: any) {
      toast.error(error.message || 'Kampanya eklenirken bir hata oluştu');
    }
  };

  const handleDelete = async (campaignId: number) => {
    if (!campaignId) {
      toast.error('Geçersiz kampanya ID');
      return;
    }

    if (window.confirm('Bu kampanyayı silmek istediğinizden emin misiniz?')) {
      try {
        const toastId = toast.loading('Kampanya siliniyor...');
        await campaignService.delete(campaignId);
        toast.success('Kampanya başarıyla silindi', { id: toastId });
        await loadData(); // Listeyi yenile
      } catch (error: any) {
        console.error('Kampanya silme hatası:', error);
        toast.error(error.message || 'Kampanya silinirken bir hata oluştu');
      }
    }
  };

  if (loading) {
    return <div className="flex justify-center items-center h-64">
      <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
    </div>;
  }

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-6">Kampanya Yönetimi</h1>
      
      {/* Kampanya Ekleme Formu */}
      <div className="bg-white p-6 rounded-lg shadow-md mb-6">
        <h2 className="text-xl font-semibold mb-4">Yeni Kampanya Ekle</h2>
        <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700">Kampanya Başlığı</label>
            <input
              type="text"
              name="title"
              value={newCampaign.title}
              onChange={handleInputChange}
              className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              required
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700">İndirim Oranı (%)</label>
            <input
              type="number"
              name="discountRate"
              value={newCampaign.discountRate}
              onChange={handleInputChange}
              min="0"
              max="100"
              className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              required
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700">Başlangıç Tarihi</label>
            <input
              type="date"
              name="startDate"
              value={newCampaign.startDate}
              onChange={handleInputChange}
              className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              required
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700">Bitiş Tarihi</label>
            <input
              type="date"
              name="endDate"
              value={newCampaign.endDate}
              onChange={handleInputChange}
              className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              required
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700">İşletme</label>
            <select
              name="businessId"
              value={newCampaign.businessId}
              onChange={handleInputChange}
              className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              required
            >
              <option value="">İşletme Seçin</option>
              {businesses.map(business => (
                <option key={business.businessId} value={business.businessId}>
                  {business.name}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700">Kategori</label>
            <select
              name="categoryId"
              value={newCampaign.categoryId}
              onChange={handleInputChange}
              className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              required
            >
              <option value="">Kategori Seçin</option>
              {categories.map(category => (
                <option key={category.categoryId} value={category.categoryId}>
                  {category.name}
                </option>
              ))}
            </select>
          </div>

          <div className="col-span-2">
            <label className="block text-sm font-medium text-gray-700">Açıklama</label>
            <textarea
              name="description"
              value={newCampaign.description}
              onChange={handleInputChange}
              rows={3}
              className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
              required
            />
          </div>

          <div className="col-span-2">
            <button
              type="submit"
              className="w-full bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
            >
              Kampanya Ekle
            </button>
          </div>
        </form>
      </div>

      {/* Kampanya Listesi */}
      <div className="bg-white rounded-lg shadow-md overflow-hidden">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Kampanya</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">İşletme</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Kategori</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Tarih</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">İndirim</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">İşlemler</th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {campaigns.map(campaign => (
              <tr key={campaign.campaignId}>
                <td className="px-6 py-4">
                  <div className="text-sm font-medium text-gray-900">{campaign.title}</div>
                  <div className="text-sm text-gray-500">{campaign.description}</div>
                </td>
                <td className="px-6 py-4">
                  <div className="text-sm text-gray-900">{campaign.business?.name || 'İşletme Yok'}</div>
                </td>
                <td className="px-6 py-4">
                  <span className="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-blue-100 text-blue-800">
                    {campaign.category?.name || 'Kategori Yok'}
                  </span>
                </td>
                <td className="px-6 py-4">
                  <div className="text-sm text-gray-900">
                    {new Date(campaign.startDate).toLocaleDateString('tr-TR')} -
                    {new Date(campaign.endDate).toLocaleDateString('tr-TR')}
                  </div>
                </td>
                <td className="px-6 py-4">
                  <div className="text-sm text-gray-900">%{campaign.discountRate}</div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                  <button 
                    onClick={() => handleDelete(campaign.campaignId)}
                    className="text-red-600 hover:text-red-800"
                  >
                    Sil
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
} 