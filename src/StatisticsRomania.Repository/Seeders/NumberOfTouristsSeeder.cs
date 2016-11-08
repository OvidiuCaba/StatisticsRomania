using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal class NumberOfTouristsSeeder : BaseSeeder
    {
        internal static List<NumberOfTourists> GetData()
        {
            var rawData = new List<string>()
                              {
                                  // 2014, 2015
                                  "2014 10 Alba 8718 7455 6047 5723 6634 6835 9336 14848 15397 19913 23753 15548 12715 12726 10123",
                                  "2014 10 Arad 19241 15721 13757 13625 13003 14886 15586 18689 19975 25910 26644 20188 17588 14388 13775",
                                  "2014 10 Arges 13345 10778 11643 9523 9080 10385 13721 15370 17745 26528 28858 19551 16387 13692 12641",
                                  "2014 10 Bacau 9197 7430 7130 6880 7176 7955 7832 9659 13093 13846 16422 12387 11173 8632 8500",
                                  "2014 10 Bihor 26768 22673 18912 12621 15268 16453 20641 27871 34384 42307 50062 35147 32282 31008 24144",
                                  "2014 10 BistritaNasaud 5813 4386 4177 3993 4142 4590 5176 8236 8377 10919 10795 8096 6399 5354 4216",
                                  "2014 10 Botosani 3077 2962 2433 2273 1833 2325 2469 3109 3730 3167 3151 3832 4341 3553 3887",
                                  "2014 10 Brasov 74162 68159 75766 78678 77000 61827 63631 76059 81892 95494 120754 95090 84156 77036 80823",
                                  "2014 10 Braila 5028 4911 4804 3468 3687 4647 4712 6260 7273 8680 8657 7036 6847 5857 4293",
                                  "2014 10 Buzau 7225 5758 4250 3808 4007 4947 4954 5959 6213 7386 7836 6812 5891 5489 4778",
                                  "2014 10 CarasSeverin 9583 7837 7001 7300 7273 7657 10793 15952 19606 24130 27231 16665 13257 11542 9344",
                                  "2014 10 Calarasi 1387 1104 1062 821 1141 1264 1332 1536 1432 2099 1958 1713 1683 1461 1107",
                                  "2014 10 Cluj 34924 31159 21462 20736 24267 28654 34560 41400 43140 42601 43397 42689 43412 36705 26678",
                                  "2014 10 Constanta 20651 14966 12077 10449 12260 14835 19676 45284 118562 308849 347708 86364 23161 19009 15318",
                                  "2014 10 Covasna 10299 7913 5213 4273 4448 4864 5987 9014 9304 11511 19265 8258 7374 6901 5935",
                                  "2014 10 Dambovita 8189 6418 6069 5024 4869 5594 5726 7693 8315 9808 9921 9121 7925 7775 7318",
                                  "2014 10 Dolj 8163 8180 7419 6262 7664 7984 8196 8571 9388 9191 9316 9409 9367 8980 7973",
                                  "2014 10 Galati 6023 5387 4214 3800 4662 5025 5544 5993 7225 6404 7316 7332 7903 6955 5484",
                                  "2014 10 Giurgiu 1558 1870 1400 1391 1275 1768 2184 1991 2674 3188 2626 2341 1797 2107 1518",
                                  "2014 10 Gorj 7996 5171 5083 4874 4997 5091 5719 6507 6287 10464 9510 7429 6021 5516 5793",
                                  "2014 10 Harghita 9565 7357 7812 7716 8484 6455 8758 14591 13773 19721 22405 16835 11713 10241 11998",
                                  "2014 10 Hunedoara 8715 9248 6494 6574 7743 9422 10849 14731 15413 19053 22277 14936 10563 10004 9369",
                                  "2014 10 Ialomita 4348 2644 1915 2106 2058 2321 2338 3932 4985 6021 6774 4067 4037 3762 2462",
                                  "2014 10 Iasi 23129 18468 13433 11825 13611 17245 17952 22457 23052 22541 20467 26342 27539 24842 18597",
                                  "2014 10 Ilfov 11300 9027 9215 8593 8444 10097 10009 11973 12469 12548 10999 12318 11036 10070 8302",
                                  "2014 10 Maramures 11554 9847 10243 7998 8391 8358 9771 13816 14593 18573 20025 15525 12790 10381 12659",
                                  "2014 10 Mehedinti 4708 3157 2568 2634 3248 3501 4942 6709 7990 10204 13477 10368 7571 5707 4620",
                                  "2014 10 Mures 36278 29147 25464 24041 27107 30696 31906 41916 43709 56337 67957 51867 47581 38208 30856",
                                  "2014 10 Neamt 14097 8578 10668 7642 7682 8155 11447 15720 20130 24752 31422 21025 13076 9288 11096",
                                  "2014 10 Olt 3491 3020 2214 2378 2136 3172 3141 3427 2811 2257 2350 2840 3231 2691 2909",
                                  "2014 10 Prahova 34618 29935 31061 30599 30703 27962 30659 37985 37113 46483 56026 46272 43008 39459 39285",
                                  "2014 10 SatuMare 8653 7808 7042 7240 7378 7270 7071 7891 8655 9296 9694 8640 7132 7157 7239",
                                  "2014 10 Salaj 2943 2760 1938 1807 2380 2414 2245 3289 3149 4320 4714 3711 3473 3091 3091",
                                  "2014 10 Sibiu 27518 26130 24394 16270 17321 19778 23330 35585 40074 63397 71199 51637 28309 35558 34429",
                                  "2014 10 Suceava 19524 16523 21965 17959 18375 15765 20887 26756 28422 37559 43873 31143 24211 19815 24327",
                                  "2014 10 Teleorman 1153 1023 662 739 774 775 824 1370 1432 1399 1318 1027 1322 1173 1061",
                                  "2014 10 Timis 29968 26131 20098 17511 19644 24674 27527 31207 30949 32019 31468 35684 34957 30944 22775",
                                  "2014 10 Tulcea 3237 2663 1605 1502 2008 2359 3284 7268 10543 10458 13257 9763 4738 2211 1685",
                                  "2014 10 Vaslui 3156 2194 2218 2124 1794 2812 3214 3368 3180 4047 3921 3992 3605 2949 2880",
                                  "2014 10 Valcea 16448 15533 15461 14356 13317 12659 20194 25687 26454 37759 43660 27271 22726 19586 21945",
                                  "2014 10 Vrancea 3958 3945 3345 2274 2370 2500 3396 3260 3545 4093 3988 3820 4012 3960 5561",
                                  "2014 10 Bucuresti 157606 137846 108723 98968 109511 145347 136609 166963 164254 156049 143682 164670 164824 148151 124971",
                                  // 2016
                                    "2016 1 Alba 8829 9336 10201 10996 14219 15775 20486 24358",
                                    "2016 1 Arad 12477 13748 15062 15820 17515 17730 20694 22356",
                                    "2016 1 Arges 11029 12039 12673 13100 15841 18799 26575 31417",
                                    "2016 1 Bacau 7362 7699 8983 10449 12048 12107 13706 15272",
                                    "2016 1 Bihor 22823 23435 23861 27564 34173 37379 48887 57358",
                                    "2016 1 BistritaNasaud 3918 4453 5175 5410 8355 10006 10752 14286",
                                    "2016 1 Botosani 2805 2797 3165 3271 3784 4053 4376 3995",
                                    "2016 1 Brasov 81591 79718 59301 69272 79765 93338 116693 139588",
                                    "2016 1 Braila 3747 4622 5963 5899 5968 6165 7810 9044",
                                    "2016 1 Buzau 3828 3893 4195 4445 5913 6428 8069 9356",
                                    "2016 1 CarasSeverin 9049 9478 8595 12732 15925 18084 25009 29070",
                                    "2016 1 Calarasi 1142 1138 1308 1249 1419 1823 2013 2387",
                                    "2016 1 Cluj 23821 29998 35128 40033 47371 46363 53076 54617",
                                    "2016 1 Constanta 12422 15187 16799 24091 37846 130042 356723 396442",
                                    "2016 1 Covasna 4043 4395 4620 6736 8199 11100 11250 12823",
                                    "2016 1 Dambovita 5382 5501 6447 7024 7934 9277 11459 10988",
                                    "2016 1 Dolj 6866 7347 8171 8287 8390 8531 8274 8070",
                                    "2016 1 Galati 4762 5436 6359 6292 7219 8189 7151 8015",
                                    "2016 1 Giurgiu 1310 1531 2633 1824 1981 2256 3185 2530",
                                    "2016 1 Gorj 5621 5545 4578 4974 5681 7234 11490 13616",
                                    "2016 1 Harghita 12179 11707 9169 11158 18025 17199 21364 26699",
                                    "2016 1 Hunedoara 7522 8727 9074 8890 11917 13435 17692 18321",
                                    "2016 1 Ialomita 1356 1826 2399 3086 3548 4688 5895 5569",
                                    "2016 1 Iasi 17064 18403 21788 23627 27142 26444 27620 27102",
                                    "2016 1 Ilfov 9097 8953 10172 10313 12053 13624 13578 12702",
                                    "2016 1 Maramures 10394 11382 10963 12511 16207 17410 22011 26306",
                                    "2016 1 Mehedinti 5367 4463 5463 6105 7624 8492 11363 15062",
                                    "2016 1 Mures 28488 32862 31767 40660 44756 43593 59237 69921",
                                    "2016 1 Neamt 9023 8596 10021 15697 17434 22886 31289 39144",
                                    "2016 1 Olt 2058 2658 3641 3315 3112 2858 2933 2770",
                                    "2016 1 Prahova 35781 38029 30673 33806 41228 43420 50564 60889",
                                    "2016 1 SatuMare 6913 6813 6730 6740 7038 7665 8088 8985",
                                    "2016 1 Salaj 1739 2248 2485 2512 3344 3492 4085 4943",
                                    "2016 1 Sibiu 23734 25805 25518 34769 41571 45877 59589 70200",
                                    "2016 1 Suceava 19743 19323 17516 20223 26989 31016 39546 48490",
                                    "2016 1 Teleorman 639 762 1097 923 1262 1038 1413 1351",
                                    "2016 1 Timis 20570 24950 31167 29297 35172 34227 32576 31828",
                                    "2016 1 Tulcea 1202 1596 1984 3256 7732 11534 11839 15817",
                                    "2016 1 Vaslui 1761 2951 2868 3198 3657 3448 3648 4095",
                                    "2016 1 Valcea 18651 19571 16236 20172 25018 28978 41863 49592",
                                    "2016 1 Vrancea 2683 2722 2810 3131 4099 3975 4479 4766",
                                    "2016 1 Bucuresti 112482 129012 149088 152709 186999 174558 163250 161193",
                              };

            var items = GetItems<NumberOfTourists>(rawData);

            return items;
        }
    }
}