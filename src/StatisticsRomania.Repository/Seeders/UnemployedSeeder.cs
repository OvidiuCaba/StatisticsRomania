﻿using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class UnemployedSeeder : BaseSeeder
    {
        internal static List<Unemployed> GetData()
        {
            var rawData = new List<string>()
                              {
                                  // 2014, 2015
                                  "2014 10 Alba 10380 10696 12508 15312 16406 15766 14846 14138 13059 10694 9812 9212 9102 9165 9394",
                                  "2014 10 Arad 6668 6004 6071 6106 5990 5778 5608 5380 5335 5275 5264 5112 4993 4983 5104",
                                  "2014 10 Arges 14693 15100 15334 15523 16024 15632 14926 14185 13715 13400 12880 12941 12962 12934 13015",
                                  "2014 10 Bacau 14309 14327 14809 14978 15309 14867 13894 13671 13722 13980 14127 14105 14009 14229 14254",
                                  "2014 10 Bihor 10159 9537 10030 10769 11111 10677 10339 9721 8734 9635 9873 9893 9965 9299 9601",
                                  "2014 10 BistritaNasaud 5713 5801 6042 5949 5787 4946 4542 4508 4258 4504 4631 4751 4645 4575 4695",
                                  "2014 10 Botosani 7446 7529 7614 7769 7795 7350 6740 6459 6639 6999 7012 6800 6960 7071 7205",
                                  "2014 10 Brasov 10818 11081 10850 11076 10888 10788 10464 9913 9677 10025 9902 9551 9903 9842 9734",
                                  "2014 10 Braila 8765 9259 9519 9781 9770 9580 9074 8766 9054 9186 9090 9029 9049 9109 9176",
                                  "2014 10 Buzau 18050 18281 18348 18286 17834 17732 17422 17282 18395 17962 18625 18720 18041 18013 18435",
                                  "2014 10 CarasSeverin 5052 5207 5277 5290 5377 5103 4651 4463 4526 4562 4583 4734 4821 4988 4870",
                                  "2014 10 Calarasi 7859 8237 8599 8532 8438 8195 7808 7359 7408 7673 7471 7410 7254 7236 7232",
                                  "2014 10 Cluj 9837 9695 9938 10365 10447 10055 9578 8953 8592 8193 8123 8022 8055 7892 7981",
                                  "2014 10 Constanta 11218 11818 11978 13002 13136 12779 10824 9752 9246 8995 8830 8775 9886 10836 10907",
                                  "2014 10 Covasna 5408 5537 5746 6061 5777 5729 5372 4931 5306 5887 5658 5498 4984 4823 5048",
                                  "2014 10 Dambovita 16027 16381 16228 15962 15696 15161 14219 13753 14080 14996 15002 14841 14397 14344 14296",
                                  "2014 10 Dolj 25891 26316 26755 27498 27830 27404 26548 25544 25322 25797 25757 24614 25356 25316 26172",
                                  "2014 10 Galati 18429 18720 19253 19169 19320 18855 18336 17830 19050 19434 19640 17753 17973 18141 17771",
                                  "2014 10 Giurgiu 6566 6547 6510 6464 6558 6650 6546 6392 6646 7154 6812 6362 6101 5923 5723",
                                  "2014 10 Gorj 10629 10348 10581 11205 12202 12117 11768 11193 12056 12292 11009 10832 10671 10680 10540",
                                  "2014 10 Harghita 7588 7975 8256 8146 7971 7452 6723 6346 6526 7044 7193 7183 7233 7260 7509",
                                  "2014 10 Hunedoara 11373 11799 12467 12897 12875 12518 11652 11627 11388 11402 11422 11515 11531 11131 11339",
                                  "2014 10 Ialomita 7340 7798 8481 8720 8752 8308 7569 7149 7062 6931 6980 7247 7104 7283 7828",
                                  "2014 10 Iasi 14790 14655 14612 14891 14773 14439 14300 13876 13584 13836 13950 13637 13424 13269 13026",
                                  "2014 10 Ilfov 2628 2594 2634 2700 2497 2507 2372 2348 2272 2326 2312 2261 2215 2175 2174",
                                  "2014 10 Maramures 7471 7153 7140 7101 7198 7065 6977 6752 7428 7949 7859 7432 7183 7167 7143",
                                  "2014 10 Mehedinti 11800 11540 11464 11921 12113 11732 11389 10789 11007 11182 11401 11214 12557 12282 12219",
                                  "2014 10 Mures 14269 14579 14039 15412 16673 17186 16869 15104 14754 13727 12561 11458 11620 11403 11384",
                                  "2014 10 Neamt 11842 12037 12114 12605 12539 12296 11749 11463 11374 11496 11530 11641 11530 11497 11571",
                                  "2014 10 Olt 14120 14550 14435 14895 15405 14598 13861 13417 13336 13776 13578 13329 13722 13934 13695",
                                  "2014 10 Prahova 15517 15575 15791 17012 17589 16854 15632 15018 14858 13759 13119 13287 13099 12905 12638",
                                  "2014 10 SatuMare 6460 6528 6816 7046 6944 6719 6233 5865 5797 5690 5695 5663 5670 5855 6037",
                                  "2014 10 Salaj 5706 5822 6235 6248 6104 5636 5279 5108 4993 5107 5123 5188 5333 5527 5589",
                                  "2014 10 Sibiu 8192 8778 8852 9005 9125 8633 8175 7908 7991 7744 7570 7629 7371 7352 7163",
                                  "2014 10 Suceava 15995 15946 16440 16977 16872 16269 15495 15817 16331 15911 15805 15206 15502 15297 15344",
                                  "2014 10 Teleorman 18037 18383 18888 19370 19777 19962 19112 18873 19138 19927 19123 18991 18546 18735 18535",
                                  "2014 10 Timis 5480 5482 5433 5294 5148 4946 4786 4493 4608 4828 4820 4932 4635 4379 4329",
                                  "2014 10 Tulcea 4440 4540 4775 4887 4834 4454 4269 3916 3759 3870 3994 3990 4127 4492 4730",
                                  "2014 10 Vaslui 16916 17056 17369 18232 18109 17855 17113 16201 15666 16628 16411 15803 15940 16394 16386",
                                  "2014 10 Valcea 9035 9157 9604 10620 11306 11187 10509 9836 10255 11158 9875 9211 8057 8165 7726",
                                  "2014 10 Vrancea 8716 8653 8841 8967 8974 8798 8360 8063 8428 8907 8663 8659 7952 7861 7877",
                                  "2014 10 Bucuresti 21997 21828 21662 21372 21258 21300 21318 21332 21328 21327 21271 21185 21090 20977 20847",
                                  // 2016
"2016 1 Alba 9301 8935 8970 8628 8620 8637 8629 8665 8140 8167 8221 8566",
"2016 1 Arad 4887 4834 4881 4837 4938 4938 5074 5114 5121 4948 4440 4937",
"2016 1 Arges 12807 12761 12478 12073 11931 12268 12510 12789 12476 12364 11906 12115",
"2016 1 Bacau 15267 14189 13466 14476 14615 14334 14605 14100 13800 13898 14180 14560",
"2016 1 Bihor 9704 9822 9223 9033 8920 8752 9037 9133 8783 8713 8047 8155",
"2016 1 BistritaNasaud 4086 4361 4204 4049 3905 3888 4173 4271 4653 5265 4477 4857",
"2016 1 Botosani 7215 7145 7079 6806 6871 6986 7195 7272 7367 7536 7590 7994",
"2016 1 Brasov 8918 9176 9829 9918 10010 10134 10113 10021 9633 9583 9364 9306",
"2016 1 Braila 9303 9161 9069 8748 8575 8452 8573 8571 8587 8516 8629 8609",
"2016 1 Buzau 17644 18100 17002 17042 17636 18014 18662 18804 18807 17993 18085 17144",
"2016 1 CarasSeverin 4809 4650 4260 3975 3875 3271 3230 2681 2355 2297 2309 2413",
"2016 1 Calarasi 6970 6894 7046 6905 6917 7032 7244 7310 7232 7148 7368 7412",
"2016 1 Cluj 8132 7576 7297 7499 7265 7077 7330 7375 6929 7027 7378 8020",
"2016 1 Constanta 10978 10183 10488 9284 8617 8109 8164 8204 8461 9530 10556 10421",
"2016 1 Covasna 4842 4879 4673 4264 3981 4160 4538 4570 4415 4278 4394 4497",
"2016 1 Dambovita 12615 12977 12892 12626 12809 13047 13496 13402 13575 13487 13491 13610",
"2016 1 Dolj 25909 26172 26460 26214 26264 26168 26365 26300 26138 26105 25702 25869",
"2016 1 Galati 17747 17544 17362 17351 17325 18563 18960 18497 18552 18577 18645 18889",
"2016 1 Giurgiu 5509 5106 4737 4792 4984 5016 5238 5323 5111 5030 5012 4809",
"2016 1 Gorj 9562 9332 8934 9137 8951 8780 8610 8590 9115 9547 10201 10095",
"2016 1 Harghita 7542 7554 7187 6672 6906 6901 7255 7647 7678 7533 7580 7829",
"2016 1 Hunedoara 10141 10918 9025 8744 8562 8695 9018 9054 9839 10406 10636 10789",
"2016 1 Ialomita 8341 8128 8208 7690 7739 7348 7232 7184 6791 6884 6904 7514",
"2016 1 Iasi 12858 12682 13091 12946 12988 13002 13285 13713 13466 13203 13192 13104",
"2016 1 Ilfov 2106 2022 1967 1870 1894 1887 1846 1911 1966 1967 1951 1962",
"2016 1 Maramures 7155 6989 6588 6400 6310 6676 6922 6870 6892 6739 6438 6409",
"2016 1 Mehedinti 12156 12494 12774 12084 11799 11532 11305 10897 10628 10177 10049 10034",
"2016 1 Mures 10884 11253 10741 10989 11286 11351 11298 11382 11124 10837 11007 10924",
"2016 1 Neamt 11994 11255 11090 10539 10479 10609 10425 10565 10768 10969 11209 11171",
"2016 1 Olt 13858 13874 13331 12977 12692 12754 13589 13926 14031 14934 14957 14897",
"2016 1 Prahova 12683 12422 12458 11906 12023 12235 12403 12656 12836 13107 12523 12718",
"2016 1 SatuMare 6121 6196 6110 5875 5918 5939 5875 5718 5651 5944 5982 6095",
"2016 1 Salaj 5615 5736 5607 5621 5604 5469 5262 5717 5695 5585 5441 5427",
"2016 1 Sibiu 7209 6914 7005 6966 6979 6716 6781 6938 6667 6577 6796 6617",
"2016 1 Suceava 15227 15620 13955 14107 14266 14312 14401 14468 14619 14715 14762 14453",
"2016 1 Teleorman 18199 17857 18574 17832 17951 18430 19464 19073 18724 17944 17284 15999",
"2016 1 Timis 4234 4297 4126 4075 4239 3799 4149 4440 4083 4039 3965 3944",
"2016 1 Tulcea 4504 4633 4689 4395 4323 4119 4254 4231 4154 4061 4358 4517",
"2016 1 Vaslui 16210 16203 16224 16392 16599 16548 16780 16868 16945 17100 16811 16791",
"2016 1 Valcea 8003 8230 8107 7647 7017 7143 8521 8868 8853 8141 8162 8153",
"2016 1 Vrancea 7992 7668 7785 7685 7755 8228 8442 8515 8135 8068 7965 7993",
"2016 1 Bucuresti 20790 20723 20483 20329 20171 20047 19840 19640 19433 18936 18706 18619",
// 2017
"2017 1 Alba 8643 8621 8449 8006 7273 6928 6371 6044 5664 5899 5933 6163",
"2017 1 Arad 5597 4993 4879 2777 2777 2821 2796 3069 3069 2901 3209 3245",
"2017 1 Arges 11992 11709 11304 10658 10111 9998 10989 11178 10942 10675 10497 10322",
"2017 1 Bacau 14920 14220 13300 12720 12503 12620 12997 13100 13120 13000 13624 14032",
"2017 1 Bihor 8875 8427 7317 7184 6810 6682 6974 6543 6507 6353 6450 6497",
"2017 1 BistritaNasaud 5128 5262 4958 4817 4662 4562 3484 3763 3851 3834 3841 4043",
"2017 1 Botosani 7192 7174 6903 6362 6038 5960 6138 6078 5655 5108 4722 4919",
"2017 1 Brasov 9338 9417 9124 8459 8295 8249 8425 7972 7700 7553 7425 7353",
"2017 1 Braila 8614 8084 7818 7419 7124 6956 6787 6697 6511 6100 6128 6294",
"2017 1 Buzau 17413 16955 17317 15681 16516 17372 17429 18325 17959 15100 14827 15001",
"2017 1 CarasSeverin 2456 2485 2361 2322 2450 2436 2636 2811 2965 3134 3183 3961",
"2017 1 Calarasi 7165 6886 6595 5943 5524 5004 5032 5071 4756 4490 4396 4487",
"2017 1 Cluj 8499 8610 8387 7679 7415 7244 7276 7862 7762 7573 7236 7460",
"2017 1 Constanta 10539 10686 10351 9689 8041 7497 7527 7554 7567 8511 8908 9099",
"2017 1 Covasna 4523 4294 3911 3709 3599 3311 3749 3873 3740 3647 3659 3612",
"2017 1 Dambovita 13593 13180 12702 12338 11275 11141 10874 10562 10435 10247 10132 10188",
"2017 1 Dolj 26192 25788 24706 23650 23327 23452 23543 24353 23681 22679 23072 23331",
"2017 1 Galati 19307 18912 17830 18107 18304 19298 19168 18204 17700 16550 15459 15245",
"2017 1 Giurgiu 4799 4749 4625 3925 3305 3350 3583 3591 3405 3054 2678 2668",
"2017 1 Gorj 10547 11085 11276 11339 10559 9440 9140 9038 8890 8670 8635 8600",
"2017 1 Harghita 7905 7949 7055 6624 6459 6716 6219 6392 6431 6358 6449 6542",
"2017 1 Hunedoara 10999 9984 10013 9820 9394 8829 8054 7553 7691 8062 7675 7898",
"2017 1 Ialomita 7638 7665 6940 6514 6248 6220 6360 6081 6268 6202 6505 6476",
"2017 1 Iasi 13490 13674 13501 13213 12730 12512 12746 12861 12583 12392 12134 11960",
"2017 1 Ilfov 1956 1889 1923 1688 1685 1718 1893 1555 1625 1461 1225 1207",
"2017 1 Maramures 6801 6947 6934 6709 6677 7033 7565 7470 7341 7113 6593 6186",
"2017 1 Mehedinti 10689 11140 10616 10412 9709 9430 10382 10129 9949 9783 9868 9777",
"2017 1 Mures 10952 10931 10924 10873 10535 10516 10237 9886 9726 9372 9301 9314",
"2017 1 Neamt 11325 11365 10876 10352 10457 10250 10009 9690 9643 9501 9377 9039",
"2017 1 Olt 14560 13965 12406 11945 11567 11290 11300 11509 11659 11743 11802 12027",
"2017 1 Prahova 12194 10691 10023 9408 7910 8010 9065 8845 9400 9153 9075 8991",
"2017 1 SatuMare 6112 6095 5853 5572 5268 5206 5018 4956 4726 4717 4723 4727",
"2017 1 Salaj 5554 5512 5546 5277 4980 5062 5419 5561 5572 4812 4503 4821",
"2017 1 Sibiu 6253 6425 5935 5980 5230 5210 5067 4878 4861 4880 4761 4770",
"2017 1 Suceava 13735 13563 12193 10989 11006 11213 10647 11749 11549 11601 11792 12379",
"2017 1 Teleorman 16155 15803 15720 15483 15393 15599 16157 16193 16204 15922 15801 14893",
"2017 1 Timis 3898 3933 3920 3869 3902 3875 3804 3729 3792 3856 3663 3540",
"2017 1 Tulcea 4490 4357 4378 3816 3596 3538 3505 3506 3459 3754 3836 3951",
"2017 1 Vaslui 16706 16056 15983 15792 15394 15158 15228 15323 14768 14702 14227 14239",
"2017 1 Valcea 8087 8010 7949 7701 7206 7081 7030 7041 7112 7251 7028 6670",
"2017 1 Vrancea 7812 7667 7623 7515 7451 7436 7543 7487 7382 7168 7035 6903",
"2017 1 Bucuresti 18593 18574 18527 18477 18456 18422 18410 18415 18410 18411 18343 18275",
// 2018
"2018 1 Alba 6105 6454 6447 5951",
"2018 1 Arad 3077 3017 3015 2731",
"2018 1 Arges 10411 10332 9740 8936",
"2018 1 Bacau 14107 13938 13513 12918",
"2018 1 Bihor 6328 6063 5940 5562",
"2018 1 BistritaNasaud 4096 4062 3844 3544",
"2018 1 Botosani 4966 4878 4708 4071",
"2018 1 Brasov 7344 7164 7060 6566",
"2018 1 Braila 6410 6366 6188 5815",
"2018 1 Buzau 14825 14315 13958 13148",
"2018 1 CarasSeverin 3340 4014 3694 3607",
"2018 1 Calarasi 4569 4505 4388 3988",
"2018 1 Cluj 7231 7058 5886 5796",
"2018 1 Constanta 9121 9093 8638 6892",
"2018 1 Covasna 3578 3448 3363 3119",
"2018 1 Dambovita 10050 9401 9758 9731",
"2018 1 Dolj 23578 23301 22603 21249",
"2018 1 Galati 14875 14749 14220 13088",
"2018 1 Giurgiu 2604 3017 2663 2448",
"2018 1 Gorj 7517 7150 6816 6130",
"2018 1 Harghita 6361 6423 6279 5858",
"2018 1 Hunedoara 8077 7796 7506 6667",
"2018 1 Ialomita 6829 6361 6043 5655",
"2018 1 Iasi 11846 11720 11329 10766",
"2018 1 Ilfov 1216 1190 1235 1352",
"2018 1 Maramures 6143 6151 6173 6196",
"2018 1 Mehedinti 10187 9700 9754 9282",
"2018 1 Mures 9105 9087 8950 8786",
"2018 1 Neamt 9477 9346 9221 9170",
"2018 1 Olt 11837 11613 11196 10240",
"2018 1 Prahova 9060 8978 8617 8098",
"2018 1 SatuMare 4755 4775 4654 4418",
"2018 1 Salaj 5091 5075 4892 4650",
"2018 1 Sibiu 4751 4728 4527 4248",
"2018 1 Suceava 12900 13051 12606 11705",
"2018 1 Teleorman 14609 14453 13840 13540",
"2018 1 Timis 3471 3555 3306 3174",
"2018 1 Tulcea 3801 3563 3180 2502",
"2018 1 Vaslui 13570 13200 12777 12191",
"2018 1 Valcea 6757 6402 5480 4997",
"2018 1 Vrancea 6738 6791 6700 6301",
"2018 1 Bucuresti 18250 18116 18023 17939",


                              };

            var items = GetItems<Unemployed>(rawData);

            return items;
        }
    }
}