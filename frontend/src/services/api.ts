import axios from 'axios';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5001/api';

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  }
});

// Token interceptor'ı
api.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

// Hata yakalama interceptor'ı
api.interceptors.response.use(
  response => response,
  error => {
    console.error('API Hatası:', error.response?.data || error.message);
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('isAdmin');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

// Hata yönetimi için yardımcı fonksiyon
const handleApiError = (error: any) => {
  if (error.response) {
    // Backend'den gelen hata mesajı
    throw new Error(error.response.data.message || 'Bir hata oluştu');
  } else if (error.request) {
    // Backend'e ulaşılamadı
    throw new Error('Sunucuya ulaşılamıyor');
  } else {
    // İstek oluşturulurken hata
    throw new Error('İstek oluşturulurken bir hata oluştu');
  }
};

export interface Business {
  businessId: number;
  name: string;
  description: string;
  address: string;
  city: string;
  phone: string;
  email: string;
  imageUrl: string | null;
  categoryId: number;
  category: Category | null;
  campaigns: Campaign[];
  isDeleted: boolean;
  createdDate: string;
}

export interface Campaign {
  campaignId: number;
  title: string;
  description: string;
  discountRate: number;
  startDate: string;
  endDate: string;
  businessId: number;
  categoryId: number;
  business: Business | null;
  category: Category | null;
  isDeleted: boolean;
  createdDate: string;
}

export interface Category {
  categoryId: number;
  name: string;
  description: string;
  businesses: Business[];
  campaigns: Campaign[];
  isDeleted: boolean;
  createdDate: string;
}

// İşletme servisleri
export const businessService = {
  getAll: async () => {
    try {
      const response = await api.get('/Business');
      console.log('İşletmeler yüklendi:', response.data);
      return response.data;
    } catch (error: any) {
      console.error('İşletmeler yüklenirken hata:', error);
      throw new Error(error.response?.data?.message || 'İşletmeler yüklenirken bir hata oluştu');
    }
  },

  getById: async (id: number): Promise<Business> => {
    try {
      const response = await api.get<Business>(`/Business/${id}`);
      console.log('Business detay verileri:', response.data);
      return response.data;
    } catch (error) {
      console.error('Business detay verileri alınırken hata:', error);
      throw error;
    }
  },

  getByCity: async (city: string): Promise<Business[]> => {
    try {
      const response = await api.get<Business[]>(`/Business/city/${city}`);
      return response.data;
    } catch (error) {
      console.error('Şehre göre business verileri alınırken hata:', error);
      throw error;
    }
  },

  getByCategory: async (categoryId: number): Promise<Business[]> => {
    try {
      const response = await api.get<Business[]>(`/Business/category/${categoryId}`);
      return response.data;
    } catch (error) {
      console.error('Kategoriye göre business verileri alınırken hata:', error);
      throw error;
    }
  },

  create: async (business: any) => {
    try {
      const response = await api.post('/Business', business);
      return response.data;
    } catch (error: any) {
      throw new Error(error.response?.data?.message || 'İşletme eklenirken bir hata oluştu');
    }
  },

  update: async (id: number, business: any) => {
    const response = await fetch(`${API_URL}/businesses/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(business),
    });
    if (!response.ok) throw new Error('İşletme güncellenemedi');
    return response.json();
  },

  delete: async (businessId: number) => {
    try {
      const response = await api.delete(`/Business/${businessId}`);
      return response.data;
    } catch (error: any) {
      throw new Error(error.response?.data?.message || 'İşletme silinirken bir hata oluştu');
    }
  },

  updateStatus: async (businessId: number, status: string) => {
    try {
      const response = await api.patch(`/Business/${businessId}/status`, { status });
      return response.data;
    } catch (error: any) {
      throw new Error(error.response?.data?.message || 'İşletme durumu güncellenirken bir hata oluştu');
    }
  }
};

// Kampanya servisleri
export const campaignService = {
  getAll: async () => {
    try {
      const response = await api.get('/Campaign');
      console.log('Kampanyalar yüklendi:', response.data);
      return response.data;
    } catch (error: any) {
      console.error('Kampanyalar yüklenirken hata:', error);
      throw new Error(error.response?.data || 'Kampanyalar yüklenirken bir hata oluştu');
    }
  },

  getById: async (id: number): Promise<Campaign> => {
    try {
      const response = await api.get<Campaign>(`/Campaign/${id}`);
      return response.data;
    } catch (error) {
      console.error('Campaign detay verileri alınırken hata:', error);
      throw error;
    }
  },

  getByBusiness: async (businessId: number): Promise<Campaign[]> => {
    try {
      const response = await api.get<Campaign[]>(`/Campaign/business/${businessId}`);
      return response.data;
    } catch (error) {
      console.error('İşletmeye göre campaign verileri alınırken hata:', error);
      throw error;
    }
  },

  getByCategory: async (categoryId: number): Promise<Campaign[]> => {
    try {
      const response = await api.get<Campaign[]>(`/Campaign/category/${categoryId}`);
      return response.data;
    } catch (error) {
      console.error('Kategoriye göre campaign verileri alınırken hata:', error);
      throw error;
    }
  },

  create: async (campaign: any) => {
    try {
      const response = await api.post('/Campaign', campaign);
      return response.data;
    } catch (error: any) {
      console.error('Kampanya eklenirken hata:', error);
      throw new Error(error.response?.data || 'Kampanya eklenirken bir hata oluştu');
    }
  },

  update: async (id: number, campaign: any) => {
    const response = await fetch(`${API_URL}/campaigns/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(campaign),
    });
    if (!response.ok) throw new Error('Kampanya güncellenemedi');
    return response.json();
  },

  delete: async (campaignId: number) => {
    try {
      const response = await api.delete(`/Campaign/${campaignId}`);
      if (!response.data && response.status !== 204) {
        throw new Error('Kampanya silinirken bir hata oluştu');
      }
      return true;
    } catch (error: any) {
      console.error('Kampanya silinirken hata:', error);
      if (error.response?.status === 404) {
        throw new Error('Kampanya bulunamadı');
      }
      throw new Error(error.response?.data || 'Kampanya silinirken bir hata oluştu');
    }
  }
};

export const categoryService = {
  getAll: async (): Promise<Category[]> => {
    try {
      const response = await api.get<Category[]>('/Category');
      console.log('Category verileri:', response.data);
      return response.data;
    } catch (error) {
      console.error('Category verileri alınırken hata:', error);
      throw error;
    }
  },

  getById: async (id: number): Promise<Category> => {
    try {
      const response = await api.get<Category>(`/Category/${id}`);
      return response.data;
    } catch (error) {
      console.error('Category detay verileri alınırken hata:', error);
      throw error;
    }
  },
};

// Kimlik doğrulama servisi
export const authService = {
  login: async (credentials: { email: string; password: string }) => {
    try {
      const response = await api.post('/auth/login', credentials);
      const { token } = response.data;
      localStorage.setItem('token', token);
      localStorage.setItem('isAdmin', 'true');
      return response.data;
    } catch (error: any) {
      throw new Error(error.response?.data?.message || 'Giriş yapılırken bir hata oluştu');
    }
  },

  logout: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('isAdmin');
    window.location.href = '/login';
  }
};

export default api; 