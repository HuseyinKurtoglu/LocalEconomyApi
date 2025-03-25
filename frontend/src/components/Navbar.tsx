'use client';

import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { useState, useEffect } from 'react';
import { FaBars, FaTimes, FaHome, FaStore, FaTags, FaPercent, FaUser, FaCog } from 'react-icons/fa';

export default function Navbar() {
  const pathname = usePathname();
  const [isOpen, setIsOpen] = useState(false);
  const [scrolled, setScrolled] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      setScrolled(window.scrollY > 20);
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const isActive = (path: string) => {
    return pathname === path ? 'text-indigo-400' : 'text-gray-400 hover:text-white';
  };

  const navItems = [
    { path: '/', label: 'Ana Sayfa', icon: FaHome },
    { path: '/businesses', label: 'İşletmeler', icon: FaStore },
    { path: '/categories', label: 'Kategoriler', icon: FaTags },
    { path: '/campaigns', label: 'Kampanyalar', icon: FaPercent },
    { path: '/about', label: 'Hakkımızda', icon: FaHome },
  ];

  return (
    <nav className={`fixed w-full z-50 transition-all duration-500 ${
      scrolled 
        ? 'bg-gray-900/80 backdrop-blur-lg shadow-lg border-b border-gray-800' 
        : 'bg-transparent'
    }`}>
      <div className="container mx-auto px-4">
        <div className="flex justify-between items-center h-20">
          <div className="group">
            <Link href="/" className="flex items-center space-x-2">
              <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-indigo-600 to-purple-600 flex items-center justify-center group-hover:scale-110 transition-transform duration-300">
                <span className="text-white font-bold text-xl">Y</span>
              </div>
              <span className="text-2xl font-bold bg-gradient-to-r from-indigo-400 to-purple-400 bg-clip-text text-transparent font-display">
                Yerel Ekonomi
              </span>
            </Link>
          </div>
          
          {/* Desktop Navigation */}
          <div className="hidden md:flex items-center space-x-8">
            <Link
              href="/"
              className={`text-lg font-medium transition-colors duration-300 ${isActive('/')}`}
            >
              <FaHome className="w-5 h-5 inline-block mr-2" />
              Ana Sayfa
            </Link>
            <Link
              href="/businesses"
              className={`${isActive('/businesses')} flex items-center space-x-3 py-3 px-4 rounded-xl hover:bg-gray-800/50 transition-all duration-300 group`}
            >
              <FaStore className="w-5 h-5 group-hover:scale-110 transition-transform duration-300" />
              <span>İşletmeler</span>
            </Link>
            <Link
              href="/campaigns"
              className={`${isActive('/campaigns')} flex items-center space-x-3 py-3 px-4 rounded-xl hover:bg-gray-800/50 transition-all duration-300 group`}
            >
              <FaPercent className="w-5 h-5 group-hover:scale-110 transition-transform duration-300" />
              <span>Kampanyalar</span>
            </Link>
            <Link
              href="/admin"
              className={`${isActive('/admin')} flex items-center space-x-3 py-3 px-4 rounded-xl hover:bg-gray-800/50 transition-all duration-300 group`}
            >
              <FaCog className="w-5 h-5 group-hover:scale-110 transition-transform duration-300" />
              <span>Admin Panel</span>
            </Link>
          </div>

          {/* Mobile Menu Button */}
          <button
            onClick={() => setIsOpen(!isOpen)}
            className="md:hidden text-gray-400 hover:text-white transition-colors duration-300"
          >
            {isOpen ? <FaTimes className="w-6 h-6" /> : <FaBars className="w-6 h-6" />}
          </button>
        </div>

        {/* Mobile Navigation */}
        <div
          className={`md:hidden transition-all duration-300 ease-in-out ${
            isOpen ? 'max-h-screen opacity-100' : 'max-h-0 opacity-0'
          } overflow-hidden`}
        >
          <div className="py-4 space-y-4">
            <Link
              href="/"
              className={`block text-lg font-medium transition-colors duration-300 ${isActive('/')}`}
              onClick={() => setIsOpen(false)}
            >
              <FaHome className="w-5 h-5 inline-block mr-2" />
              Ana Sayfa
            </Link>
            <Link
              href="/businesses"
              className={`${isActive('/businesses')} flex items-center space-x-3 py-3 px-4 rounded-xl hover:bg-gray-800/50 transition-all duration-300 group`}
              onClick={() => setIsOpen(false)}
            >
              <FaStore className="w-5 h-5 group-hover:scale-110 transition-transform duration-300" />
              <span>İşletmeler</span>
            </Link>
            <Link
              href="/campaigns"
              className={`${isActive('/campaigns')} flex items-center space-x-3 py-3 px-4 rounded-xl hover:bg-gray-800/50 transition-all duration-300 group`}
              onClick={() => setIsOpen(false)}
            >
              <FaPercent className="w-5 h-5 group-hover:scale-110 transition-transform duration-300" />
              <span>Kampanyalar</span>
            </Link>
            <Link
              href="/admin"
              className={`${isActive('/admin')} flex items-center space-x-3 py-3 px-4 rounded-xl hover:bg-gray-800/50 transition-all duration-300 group`}
              onClick={() => setIsOpen(false)}
            >
              <FaCog className="w-5 h-5 group-hover:scale-110 transition-transform duration-300" />
              <span>Admin Panel</span>
            </Link>
          </div>
        </div>
      </div>
    </nav>
  );
} 