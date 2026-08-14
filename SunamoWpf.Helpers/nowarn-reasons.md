# NoWarn — důvody

## CS0067 — event je nikdy vyvolán (never used)
**Zdroj:** `WpfApp.StatusSetted` a `TextBoxBackend.ScrollToLine` jsou veřejné eventy, na které se externě přihlašuje (subscribe/unsubscribe), ale uvnitř knihovny už nejsou vyvolávány (nahrazeny přímým voláním, viz zakomentovaný kód u `ScrollToLine`).
**Proč nelze opravit bez rizika:** jde o veřejné API NuGet balíčku; smazání eventu by byla breaking change pro odběratele, kteří se na něj mohli přihlásit.
**Kdy přehodnotit:** při major verzi balíčku, kdy je breaking change přípustná.

## CA1416 — platformově specifické API na Windows-only assembly
**Zdroj:** `TargetFramework` je `net10.0-windows7.0`, celá assembly je jen pro Windows (GDI+, registry, EventLog, WMI, AvalonEdit), přesto analyzer hlásí CA1416 napříč knihovnou.
**Proč nelze opravit bez rizika:** `[SupportedOSPlatform("windows")]` na sdílených helper třídách (např. `TextBoxHelper`) se kaskádovitě propaguje na všechny volající projekty (SunamoWpf.Controls atd.) a počet warningů násobně roste místo klesá.
**Kdy přehodnotit:** pokud .NET SDK v budoucnu začne CA1416 automaticky potlačovat na základě Windows-specific TFM.

## CS8600, CS8602, CS8625, CS8604, CS8618, CS8622, CS8603, CS8601, CS8073 — nullable reference warningy
**Zdroj:** starý WPF kód (extrahovaný z monolitu `SunamoWpf`) s `Nullable=enable` zapnutým dodatečně, stovky call sites napříč celou knihovnou.
**Proč nelze opravit plošně:** vyžaduje individuální ověření nullability u stovek míst bez možnosti reálného otestování chování každého helperu.
**Kdy přehodnotit:** při postupné revizi nullability jednotlivých helperů.
