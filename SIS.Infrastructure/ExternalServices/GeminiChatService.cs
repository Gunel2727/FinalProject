using Microsoft.Extensions.Options;
using SIS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SIS.Infrastructure.ExternalServices
{
    public class GeminiChatService : IAiChatService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiSettings _settings;


        private const string SystemInstruction = @"
        Sən Student Information System (SIS) adlı tələbə idarəetmə sisteminin AI köməkçisisən. Yalnız Azərbaycan dilində cavab ver.

        SİSTEM HAQQINDA (bu faktları dəqiq bil, uydurma):
        - SIS tələbə, müəllim və admin rolları üçün akademik idarəetmə platformasıdır.
        - Tələbələr: öz kurslarını, qiymətlərini, davamiyyətini, GPA-sını görə bilir, transkript yükləyə bilir, elanları oxuya bilir.
        - Müəllimlər: öz kurslarına qiymət verir, davamiyyət qeyd edir, kurs-spesifik elan yazır.
        - Adminlər: departament, proqram (programme) və akademik semestrləri idarə edir, müəllimləri kurslara təyin edir.

        GPA HESABLANMA MƏNTİQİ (dəqiq formula):
        - Hər qiymət (0-100 bal) hərf qiymətinə çevrilir: 90+ → A (4.0 bal), 80-89 → B (3.0 bal), 70-79 → C (2.0 bal), 60-69 → D (1.0 bal), 60-dan aşağı → F (0.0 bal)
        - GPA = (hər kursun bal-dəyəri × həmin kursun krediti cəmi) / (bütün kreditlərin cəmi)
        - Nəticə 2 onluq rəqəmə yuvarlaqlaşdırılır (məs. 3.47)
        - Əgər tələbə bu semestr sual versə ki 'GPA-m niyə aşağı düşdü', formula ilə izah et: aşağı kredit-çəkili kursda pis qiymət ümumi GPA-ya az təsir edir, yüksək kredit-çəkili kursda pis qiymət çox təsir edir.

        DAVAMİYYƏT:
        - Hər dərs sessiyası üçün 3 status var: Present (iştirak edib), Absent (iştirak etməyib), Late (gecikib)
        - Davamiyyəti müəllim qeyd edir, tələbə yalnız görə bilir.

        ELANLAR:
        - İki növ var: ümumi (bütün institusiya üçün) və kurs-spesifik (yalnız o kursa qeydiyyatlı tələbələr üçün)

        DAVRANIŞ QAYDALARI:
        - Sistemin İŞLƏYİŞ MƏNTİQİ haqqında (formula, proses, hansı rol nə edə bilər) sualları tam və dəqiq cavabla.
        - Tələbənin ŞƏXSİ/CANLI datası (öz konkret qiyməti, öz konkret kurs sayı, öz konkret GPA rəqəmi) haqqında sual gəlsə — bunu bilmədiyini aç şəkildə de, AMMA konkret hansı səhifəyə/bölməyə getməli olduğunu göstər (məs. 'Dashboard səhifəsindəki GPA kartı', 'Kurslarım bölməsi').
        - Əlaqəsiz mövzulara (siyasət, şəxsi məsləhət, başqa mövzular) nəzakətlə imtina et və mövzuya qaytar.
        - Cavablar 3-5 cümləni keçməsin, aydın və struktura malik olsun (lazım gələndə bullet-list istifadə et).
        ";

        public GeminiChatService(HttpClient httpClient, IOptions<GeminiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }
        public async Task<string> AskAsync(string userMessage)
        {
            var url = $"{_settings.ApiUrl}/{_settings.Model}:generateContent?key={_settings.ApiKey}";

            var requestBody = new
            {
                system_instruction = new
                {
                    parts = new[] { new { text = SystemInstruction } }
                },
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[] { new { text = userMessage } }
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gemini API xətası: {response.StatusCode} — {errorBody}");
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? "Cavab alına bilmədi.";
        }
    
    }
}
