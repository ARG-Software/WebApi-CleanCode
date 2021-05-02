CREATE INDEX "IX_Royaltytypealias_Name_lower" ON public."RoyaltyTypeAlias" using btree(lower("Name"));
CREATE INDEX "IX_Territoryalias_Name_lower" ON public."TerritoryAlias" using btree(lower("Name"));
CREATE INDEX "IX_Songalias_Name_lower" ON public."SongAlias" using btree(lower("Name"));
CREATE INDEX "IX_Sourcesongcode_Code_lower" ON public."SourceSongCode" using btree(lower("Code"));
CREATE INDEX "IX_Societyalias_Name_lower" ON public."SocietyAlias" using btree(lower("Name"));
CREATE INDEX "IX_Publisheralias_Name_lower" ON public."PublisherAlias" using btree(lower("Name"));
CREATE INDEX "IX_Platformtieralias_Name_lower" ON public."PlatformTierAlias" using btree(lower("Name"));
CREATE INDEX "IX_Episodealias_Name_lower" ON public."EpisodeAlias" using btree(lower("Name"));
CREATE INDEX "IX_Labelalias_Name_lower" ON public."LabelAlias" using btree(lower("Name"));
CREATE INDEX "IX_Productiontitlealias_Name_lower" ON public."ProductionTitleAlias" using btree(lower("Name"));
CREATE INDEX "IX_Isrc_Code" ON public."ISRC" using btree(lower("Code"));
CREATE INDEX "IX_Iswc_Code" ON public."ISWC" using btree(lower("Code"));





