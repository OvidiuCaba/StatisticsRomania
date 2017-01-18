﻿using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    public class UnemployedSeeder : BaseSeeder
    {
        public static List<Data> GetData()
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
                                    "2016 1 Alba 9301 8935 8970 8628 8620 8637 8629 8665 8140 8167",
                                    "2016 1 Arad 4887 4834 4881 4837 4938 4938 5074 5114 5121 4948",
                                    "2016 1 Arges 12807 12761 12478 12073 11931 12268 12510 12789 12476 12364",
                                    "2016 1 Bacau 15267 14189 13466 14476 14615 14334 14605 14100 13800 13898",
                                    "2016 1 Bihor 9704 9822 9223 9033 8920 8752 9037 9133 8783 8713",
                                    "2016 1 BistritaNasaud 4086 4361 4204 4049 3905 3888 4173 4271 4653 5265",
                                    "2016 1 Botosani 7215 7145 7079 6806 6871 6986 7195 7272 7367 7536",
                                    "2016 1 Brasov 8918 9176 9829 9918 10010 10134 10113 10021 9633 9583",
                                    "2016 1 Braila 9303 9161 9069 8748 8575 8452 8573 8571 8587 8516",
                                    "2016 1 Buzau 17644 18100 17002 17042 17636 18014 18662 18804 18807 17993",
                                    "2016 1 CarasSeverin 4809 4650 4260 3975 3875 3271 3230 2681 2355 2297",
                                    "2016 1 Calarasi 6970 6894 7046 6905 6917 7032 7244 7310 7232 7148",
                                    "2016 1 Cluj 8132 7576 7297 7499 7265 7077 7330 7375 6929 7027",
                                    "2016 1 Constanta 10978 10183 10488 9284 8617 8109 8164 8204 8461 9530",
                                    "2016 1 Covasna 4842 4879 4673 4264 3981 4160 4538 4570 4415 4278",
                                    "2016 1 Dambovita 12615 12977 12892 12626 12809 13047 13496 13402 13575 13487",
                                    "2016 1 Dolj 25909 26172 26460 26214 26264 26168 26365 26300 26138 26105",
                                    "2016 1 Galati 17747 17544 17362 17351 17325 18563 18960 18497 18552 18577",
                                    "2016 1 Giurgiu 5509 5106 4737 4792 4984 5016 5238 5323 5111 5030",
                                    "2016 1 Gorj 9562 9332 8934 9137 8951 8780 8610 8590 9115 9547",
                                    "2016 1 Harghita 7542 7554 7187 6672 6906 6901 7255 7647 7678 7533",
                                    "2016 1 Hunedoara 10141 10918 9025 8744 8562 8695 9018 9054 9839 10406",
                                    "2016 1 Ialomita 8341 8128 8208 7690 7739 7348 7232 7184 6791 6884",
                                    "2016 1 Iasi 12858 12682 13091 12946 12988 13002 13285 13713 13466 13203",
                                    "2016 1 Ilfov 2106 2022 1967 1870 1894 1887 1846 1911 1966 1967",
                                    "2016 1 Maramures 7155 6989 6588 6400 6310 6676 6922 6870 6892 6739",
                                    "2016 1 Mehedinti 12156 12494 12774 12084 11799 11532 11305 10897 10628 10177",
                                    "2016 1 Mures 10884 11253 10741 10989 11286 11351 11298 11382 11124 10837",
                                    "2016 1 Neamt 11994 11255 11090 10539 10479 10609 10425 10565 10768 10969",
                                    "2016 1 Olt 13858 13874 13331 12977 12692 12754 13589 13926 14031 14934",
                                    "2016 1 Prahova 12683 12422 12458 11906 12023 12235 12403 12656 12836 13107",
                                    "2016 1 SatuMare 6121 6196 6110 5875 5918 5939 5875 5718 5651 5944",
                                    "2016 1 Salaj 5615 5736 5607 5621 5604 5469 5262 5717 5695 5585",
                                    "2016 1 Sibiu 7209 6914 7005 6966 6979 6716 6781 6938 6667 6577",
                                    "2016 1 Suceava 15227 15620 13955 14107 14266 14312 14401 14468 14619 14715",
                                    "2016 1 Teleorman 18199 17857 18574 17832 17951 18430 19464 19073 18724 17944",
                                    "2016 1 Timis 4234 4297 4126 4075 4239 3799 4149 4440 4083 4039",
                                    "2016 1 Tulcea 4504 4633 4689 4395 4323 4119 4254 4231 4154 4061",
                                    "2016 1 Vaslui 16210 16203 16224 16392 16599 16548 16780 16868 16945 17100",
                                    "2016 1 Valcea 8003 8230 8107 7647 7017 7143 8521 8868 8853 8141",
                                    "2016 1 Vrancea 7992 7668 7785 7685 7755 8228 8442 8515 8135 8068",
                                    "2016 1 Bucuresti 20790 20723 20483 20329 20171 20047 19840 19640 19433 18936",
                              };

            var items = GetItems("Unemployed", rawData);

            return items;
        }
    }
}