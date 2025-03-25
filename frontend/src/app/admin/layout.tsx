'use client';

import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import Link from 'next/link';
import { FaStore, FaPercent, FaSignOutAlt } from 'react-icons/fa';

export default function AdminLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const router = useRouter();

  // TODO: Gerçek kimlik doğrulama kontrolü eklenecek
  useEffect(() => {
    // Burada admin kontrolü yapılacak
    const isAdmin = localStorage.getItem('isAdmin') === 'true';
    if (!isAdmin) {
      router.push('/login');
    }
  }, [router]);

  const handleLogout = () => {
    localStorage.removeItem('isAdmin');
    router.push('/login');
  };

  return (
    <div className="min-h-screen bg-gray-50 flex flex-col">
      <main className="container mx-auto px-4 py-8 flex-grow">
        {children}
      </main>

      {/* Alt Navbar */}
      <nav className="bg-white border-t border-gray-200">
        <div className="container mx-auto px-4">
          <div className="flex items-center justify-between h-16">
            <div className="flex items-center space-x-8">
              <Link href="/admin/businesses" className="text-gray-600 hover:text-gray-900 px-3 py-2 rounded-md text-sm font-medium flex items-center">
                <FaStore className="mr-2" />
                İşletmeler
              </Link>
              <Link href="/admin/campaigns" className="text-gray-600 hover:text-gray-900 px-3 py-2 rounded-md text-sm font-medium flex items-center">
                <FaPercent className="mr-2" />
                Kampanyalar
              </Link>
            </div>
            <button
              onClick={handleLogout}
              className="text-gray-600 hover:text-gray-900 px-3 py-2 rounded-md text-sm font-medium flex items-center"
            >
              <FaSignOutAlt className="mr-2" />
              Çıkış Yap
            </button>
          </div>
        </div>
      </nav>
    </div>
  );
} 