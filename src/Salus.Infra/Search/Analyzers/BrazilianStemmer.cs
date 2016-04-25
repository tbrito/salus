/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

/*
 * A stemmer for Brazilian words.
 */
namespace Salus.Infra.Search.Analyzers
{
    public class BrazilianStemmerCustom
    {
        /*
         * Changed term
         */
        private string term;
        private string ct;
        private string r1;
        private string r2;
        private string rv;

        /*
         * Stemms the given term to an unique <tt>discriminator</tt>.
         *
         * <param name="term"> The term that should be stemmed.</param>
         * <returns>     Discriminator for <tt>term</tt></returns>
         */
        public string Stem(string term)
        {
            // creates CT
            this.CreateCT(term);

            if (!this.IsIndexable(this.ct))
            {
                return null;
            }

            if (!this.IsStemmable(this.ct))
            {
                //// Customizacao do Ecm. Se nao for uma palavra, 
                //// provavelmente é um cpf, cep, cnpj ou algo assim, remove pontos, traços e etc.
                return this.RemoveMascara(this.ct);
            }

            this.r1 = this.GetR1(this.ct);
            this.r2 = this.GetR1(this.r1);
            this.rv = this.GetRV(this.ct);
            this.term = term + ";" + this.ct;

            bool altered = this.Step1();

            if (!altered)
            {
                altered = this.Step2();
            }

            if (altered)
            {
                this.Step3();
            }
            else
            {
                this.Step4();
            }

            this.Step5();

            return this.ct;
        }

        /*
         * For log and debug purpose
         *
         * <returns> TERM, CT, RV, R1 and R2</returns>
         */
        public string Log()
        {
            return " (TERM = " + this.term + ")" +
                   " (CT = " + this.ct + ")" +
                   " (RV = " + this.rv + ")" +
                   " (R1 = " + this.r1 + ")" +
                   " (R2 = " + this.r2 + ")";
        }

        private string RemoveMascara(string ct)
        {
            return ct.Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Replace("-", string.Empty)
                .Replace("\\", string.Empty)
                .Replace("/", string.Empty);
        }

        /*
         * Checks a term if it can be processed correctly.
         *
         * <returns> true if, and only if, the given term consists in letters.</returns>
         */
        private bool IsStemmable(string term)
        {
            for (int c = 0; c < term.Length; c++)
            {
                // Discard terms that contain non-letter characters.
                if (!char.IsLetter(term[c]))
                {
                    return false;
                }
            }

            return true;
        }

        /*
         * Checks a term if it can be processed indexed.
         *
         * <returns> true if it can be indexed</returns>
         */
        private bool IsIndexable(string term)
        {
            return (term.Length < 30) && (term.Length > 2);
        }

        /*
         * See if string is 'a','e','i','o','u'
       *
       * <returns>true if is vowel</returns>
         */
        private bool IsVowel(char value)
        {
            return (value == 'a') ||
                   (value == 'e') ||
                   (value == 'i') ||
                   (value == 'o') ||
                   (value == 'u');
        }

        /*
         * Gets R1
       *
       * R1 - is the region after the first non-vowel follwing a vowel,
       *      or is the null region at the end of the word if there is
       *      no such non-vowel.
       *
       * <returns>null or a string representing R1</returns>
         */
        private string GetR1(string value)
        {
            int j;

            // be-safe !!!
            if (value == null)
            {
                return null;
            }

            // find 1st vowel
            int i = value.Length - 1;
            for (j = 0; j < i; j++)
            {
                if (this.IsVowel(value[j]))
                {
                    break;
                }
            }

            if (!(j < i))
            {
                return null;
            }

            // find 1st non-vowel
            for (; j < i; j++)
            {
                if (!this.IsVowel(value[j]))
                {
                    break;
                }
            }

            if (!(j < i))
            {
                return null;
            }

            return value.Substring(j + 1);
        }

        /*
         * Gets RV
       *
       * RV - IF the second letter is a consoant, RV is the region after
       *      the next following vowel,
       *
       *      OR if the first two letters are vowels, RV is the region
       *      after the next consoant,
       *
       *      AND otherwise (consoant-vowel case) RV is the region after
       *      the third letter.
       *
       *      BUT RV is the end of the word if this positions cannot be
       *      found.
       *
       * <returns>null or a string representing RV</returns>
         */
        private string GetRV(string value)
        {
            int j;

            // be-safe !!!
            if (value == null)
            {
                return null;
            }

            int i = value.Length - 1;

            // RV - IF the second letter is a consoant, RV is the region after
            //      the next following vowel,
            if ((i > 0) && !this.IsVowel(value[1]))
            {
                // find 1st vowel
                for (j = 2; j < i; j++)
                {
                    if (this.IsVowel(value[j]))
                    {
                        break;
                    }
                }

                if (j < i)
                {
                    return value.Substring(j + 1);
                }
            }

            // RV - OR if the first two letters are vowels, RV is the region
            //      after the next consoant,
            if ((i > 1) &&
                this.IsVowel(value[0]) &&
                this.IsVowel(value[1]))
            {
                // find 1st consoant
                for (j = 2; j < i; j++)
                {
                    if (!this.IsVowel(value[j]))
                    {
                        break;
                    }
                }

                if (j < i)
                {
                    return value.Substring(j + 1);
                }
            }

            // RV - AND otherwise (consoant-vowel case) RV is the region after
            //      the third letter.
            if (i > 2)
            {
                return value.Substring(3);
            }

            return null;
        }

        /*
       * 1) Turn to lowercase
       * 2) Remove accents
       * 3) ã -> a ; õ -> o
       * 4) ç -> c
       *
       * <returns>null or a string transformed</returns>
         */
        private string ChangeTerm(string value)
        {
            int j;
            string r = string.Empty;

            // be-safe !!!
            if (value == null)
            {
                return null;
            }

            value = value.ToLower();
            for (j = 0; j < value.Length; j++)
            {
                if ((value[j] == 'á') ||
                    (value[j] == 'â') ||
                    (value[j] == 'ã'))
                {
                    r = r + "a"; 
                    continue;
                }

                if ((value[j] == 'é') ||
                    (value[j] == 'ê'))
                {
                    r = r + "e"; 
                    continue;
                }

                if (value[j] == 'í')
                {
                    r = r + "i"; 
                    continue;
                }

                if ((value[j] == 'ó') ||
                    (value[j] == 'ô') ||
                    (value[j] == 'õ'))
                {
                    r = r + "o"; 
                    continue;
                }

                if ((value[j] == 'ú') ||
                    (value[j] == 'ü'))
                {
                    r = r + "u"; 
                    continue;
                }

                if (value[j] == 'ç')
                {
                    r = r + "c"; 
                    continue;
                }

                if (value[j] == 'ñ')
                {
                    r = r + "n"; 
                    continue;
                }

                r = r + value[j];
            }

            return r;
        }

        /*
       * Check if a string ends with a suffix
       *
       * <returns>true if the string ends with the specified suffix</returns>
         */
        private bool Suffix(string value, string suffix)
        {
            // be-safe !!!
            if ((value == null) || (suffix == null))
            {
                return false;
            }

            if (suffix.Length > value.Length)
            {
                return false;
            }

            return value.Substring(value.Length - suffix.Length).Equals(suffix);
        }

        /*
       * Replace a string suffix by another
       *
       * <returns>the replaced string</returns>
         */
        private string ReplaceSuffix(string value, string toreplace, string changeTo)
        {
            // be-safe !!!
            if ((value == null) ||
                (toreplace == null) ||
                (changeTo == null))
            {
                return value;
            }

            string vvalue = this.RemoveSuffix(value, toreplace);

            if (value.Equals(vvalue))
            {
                return value;
            }
            
            return vvalue + changeTo;
        }

        /*
        * Remove a string suffix
        *
        * <returns>the string without the suffix</returns>
        */
        private string RemoveSuffix(string value, string toremove)
        {
            // be-safe !!!
            if ((value == null) ||
                (toremove == null) ||
                !this.Suffix(value, toremove))
            {
                return value;
            }

            return value.Substring(0, value.Length - toremove.Length);
        }

        /*
       * See if a suffix is preceded by a string
       *
       * <returns>true if the suffix is preceded</returns>
         */
        private bool SuffixPreceded(string value, string suffix, string preceded)
        {
            // be-safe !!!
            if ((value == null) ||
                (suffix == null) ||
                (preceded == null) ||
                !this.Suffix(value, suffix))
            {
                return false;
            }

            return this.Suffix(this.RemoveSuffix(value, suffix), preceded);
        }
        
        /*
         * Creates CT (changed term) , substituting * 'ã' and 'õ' for 'a~' and 'o~'.
         */
        private void CreateCT(string term)
        {
            this.ct = this.ChangeTerm(term);

            if (this.ct.Length < 2)
            {
                return;
            }

            // if the first character is ... , remove it
            if ((this.ct[0] == '"') ||
                (this.ct[0] == '\'') ||
                (this.ct[0] == '-') ||
                (this.ct[0] == ',') ||
                (this.ct[0] == ';') ||
                (this.ct[0] == '.') ||
                (this.ct[0] == '?') ||
                (this.ct[0] == '!'))
            {
                this.ct = this.ct.Substring(1);
            }

            if (this.ct.Length < 2)
            {
                return;
            }

            // if the last character is ... , remove it
            if ((this.ct[this.ct.Length - 1] == '-') ||
                (this.ct[this.ct.Length - 1] == ',') ||
                (this.ct[this.ct.Length - 1] == ';') ||
                (this.ct[this.ct.Length - 1] == '.') ||
                (this.ct[this.ct.Length - 1] == '?') ||
                (this.ct[this.ct.Length - 1] == '!') ||
                (this.ct[this.ct.Length - 1] == '\'') ||
                (this.ct[this.ct.Length - 1] == '"'))
            {
                this.ct = this.ct.Substring(0, this.ct.Length - 1);
            }
        }
        
        /*
         * Standart suffix removal.
       * Search for the longest among the following suffixes, and perform
       * the following actions:
       *
       * <returns>false if no ending was removed</returns>
         */
        private bool Step1()
        {
            ////if (this.ct == null) return false;

            ////// suffix lenght = 7
            ////if (this.Suffix(this.ct, "uciones") && this.Suffix(this.r2, "uciones"))
            ////{
            ////    this.ct = this.ReplaceSuffix(this.ct, "uciones", "u"); 
            ////    return true;
            ////}

            ////// suffix lenght = 6
            ////if (this.ct.Length >= 6)
            ////{
            ////    if (this.Suffix(this.ct, "imentos") && this.Suffix(this.r2, "imentos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "imentos"); 
            ////        return true;
            ////    }

            ////    if (this.Suffix(this.ct, "amentos") && this.Suffix(this.r2, "amentos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "amentos"); 
            ////        return true;
            ////    }

            ////    if (this.Suffix(this.ct, "adores") && this.Suffix(this.r2, "adores"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "adores"); 
            ////        return true;
            ////    }
            ////    if (this.Suffix(this.ct, "adoras") && this.Suffix(this.r2, "adoras"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "adoras"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "logias") && this.Suffix(this.r2, "logias"))
            ////    {
            ////        this.ReplaceSuffix(this.ct, "logias", "log"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "encias") && this.Suffix(this.r2, "encias"))
            ////    {
            ////        this.ct = this.ReplaceSuffix(this.ct, "encias", "ente"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "amente") && this.Suffix(this.r1, "amente"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "amente"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "idades") && this.Suffix(this.r2, "idades"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "idades"); return true;
            ////    }
            ////}

            ////// suffix lenght = 5
            ////if (this.ct.Length >= 5)
            ////{
            ////    if (this.Suffix(this.ct, "acoes") && this.Suffix(this.r2, "acoes"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "acoes"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "imento") && this.Suffix(this.r2, "imento"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "imento"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "amento") && this.Suffix(this.r2, "amento"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "amento"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "adora") && this.Suffix(this.r2, "adora"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "adora"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ismos") && this.Suffix(this.r2, "ismos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ismos"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "istas") && this.Suffix(this.r2, "istas"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "istas"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "logia") && this.Suffix(this.r2, "logia"))
            ////    {
            ////        this.ct = this.ReplaceSuffix(this.ct, "logia", "log"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ucion") && this.Suffix(this.r2, "ucion"))
            ////    {
            ////        this.ct = this.ReplaceSuffix(this.ct, "ucion", "u"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "encia") && this.Suffix(this.r2, "encia"))
            ////    {
            ////        this.ct = this.ReplaceSuffix(this.ct, "encia", "ente"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "mente") && this.Suffix(this.r2, "mente"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "mente"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "idade") && this.Suffix(this.r2, "idade"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "idade"); return true;
            ////    }
            ////}

            ////// suffix lenght = 4
            ////if (this.ct.Length >= 4)
            ////{
            ////    if (this.Suffix(this.ct, "acao") && this.Suffix(this.r2, "acao"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "acao"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ezas") && this.Suffix(this.r2, "ezas"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ezas"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "icos") && this.Suffix(this.r2, "icos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "icos"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "icas") && this.Suffix(this.r2, "icas"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "icas"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ismo") && this.Suffix(this.r2, "ismo"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ismo"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "avel") && this.Suffix(this.r2, "avel"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "avel"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ivel") && this.Suffix(this.r2, "ivel"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ivel"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ista") && this.Suffix(this.r2, "ista"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ista"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "osos") && this.Suffix(this.r2, "osos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "osos"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "osas") && this.Suffix(this.r2, "osas"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "osas"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ador") && this.Suffix(this.r2, "ador"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ador"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ivas") && this.Suffix(this.r2, "ivas"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ivas"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ivos") && this.Suffix(this.r2, "ivos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ivos"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "iras") &&
            ////        this.Suffix(this.rv, "iras") &&
            ////        this.SuffixPreceded(this.ct, "iras", "e"))
            ////    {
            ////        this.ct = this.ReplaceSuffix(this.ct, "iras", "ir"); return true;
            ////    }
            ////}

            ////// suffix lenght = 3
            ////if (this.ct.Length >= 3)
            ////{
            ////    if (this.Suffix(this.ct, "eza") && this.Suffix(this.r2, "eza"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eza"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ico") && this.Suffix(this.r2, "ico"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ico"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ica") && this.Suffix(this.r2, "ica"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ica"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "oso") && this.Suffix(this.r2, "oso"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "oso"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "osa") && this.Suffix(this.r2, "osa"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "osa"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "iva") && this.Suffix(this.r2, "iva"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iva"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ivo") && this.Suffix(this.r2, "ivo"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ivo"); return true;
            ////    }
            ////    if (this.Suffix(this.ct, "ira") &&
            ////        this.Suffix(this.rv, "ira") &&
            ////        this.SuffixPreceded(this.ct, "ira", "e"))
            ////    {
            ////        this.ct = this.ReplaceSuffix(this.ct, "ira", "ir"); return true;
            ////    }
            ////}

            // no ending was removed by step1
            return false;
        }

        /*
         * Verb suffixes.
       *
       * Search for the longest among the following suffixes in RV,
       * and if found, delete.
       *
       * <returns>false if no ending was removed</returns>
        */
        private bool Step2()
        {
            ////if (this.rv == null) return false;

            ////// suffix lenght = 7
            ////if (this.rv.Length >= 7)
            ////{
            ////    if (this.Suffix(this.rv, "issemos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "issemos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "essemos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "essemos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "assemos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "assemos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ariamos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ariamos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eriamos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eriamos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iriamos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iriamos"); return true;
            ////    }
            ////}

            ////// suffix lenght = 6
            ////if (this.rv.Length >= 6)
            ////{
            ////    if (this.Suffix(this.rv, "iremos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iremos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eremos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eremos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "aremos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "aremos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "avamos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "avamos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iramos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iramos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eramos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eramos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "aramos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "aramos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "asseis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "asseis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "esseis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "esseis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "isseis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "isseis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "arieis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "arieis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "erieis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "erieis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "irieis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "irieis"); return true;
            ////    }
            ////}
            
            ////// suffix lenght = 5
            ////if (this.rv.Length >= 5)
            ////{
            ////    if (this.Suffix(this.rv, "irmos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "irmos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iamos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iamos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "armos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "armos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ermos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ermos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "areis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "areis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ereis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ereis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ireis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ireis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "asses"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "asses"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "esses"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "esses"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "isses"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "isses"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "astes"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "astes"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "assem"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "assem"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "essem"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "essem"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "issem"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "issem"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ardes"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ardes"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "erdes"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "erdes"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "irdes"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "irdes"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ariam"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ariam"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eriam"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eriam"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iriam"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iriam"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "arias"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "arias"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "erias"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "erias"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "irias"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "irias"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "estes"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "estes"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "istes"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "istes"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "areis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "areis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "aveis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "aveis"); return true;
            ////    }
            ////}

            ////// suffix lenght = 4
            ////if (this.rv.Length >= 4)
            ////{
            ////    if (this.Suffix(this.rv, "aria"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "aria"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eria"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eria"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iria"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iria"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "asse"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "asse"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "esse"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "esse"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "isse"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "isse"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "aste"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "aste"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "este"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "este"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iste"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iste"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "arei"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "arei"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "erei"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "erei"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "irei"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "irei"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "aram"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "aram"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eram"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eram"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iram"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iram"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "avam"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "avam"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "arem"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "arem"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "erem"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "erem"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "irem"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "irem"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ando"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ando"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "endo"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "endo"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "indo"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "indo"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "arao"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "arao"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "erao"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "erao"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "irao"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "irao"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "adas"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "adas"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "idas"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "idas"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "aras"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "aras"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eras"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eras"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iras"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iras"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "avas"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "avas"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ares"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ares"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eres"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eres"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ires"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ires"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ados"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ados"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "idos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "idos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "amos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "amos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "emos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "emos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "imos"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "imos"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iras"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iras"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ieis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ieis"); return true;
            ////    }
            ////}

            ////// suffix lenght = 3
            ////if (this.rv.Length >= 3)
            ////{
            ////    if (this.Suffix(this.rv, "ada"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ada"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ida"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ida"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ara"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ara"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "era"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "era"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ira"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ava"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iam"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iam"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ado"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ado"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ido"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ido"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ias"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ias"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ais"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ais"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eis"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eis"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ira"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ira"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ear"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ear"); return true;
            ////    }
            ////}

            ////// suffix lenght = 2
            ////if (this.rv.Length >= 2)
            ////{
            ////    if (this.Suffix(this.rv, "ia"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ia"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ei"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ei"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "am"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "am"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "em"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "em"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ar"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ar"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "er"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "er"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ir"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ir"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "as"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "as"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "es"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "es"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "is"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "is"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "eu"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "eu"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iu"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iu"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "iu"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "iu"); return true;
            ////    }
            ////    if (this.Suffix(this.rv, "ou"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "ou"); return true;
            ////    }
            ////}

            // no ending was removed by step2
            return false;
        }

        /*
        * Delete suffix 'i' if in RV and preceded by 'c'
        *
        */
        private void Step3()
        {
            if (this.rv == null)
            {
                return;
            }

            if (this.Suffix(this.rv, "i") && this.SuffixPreceded(this.rv, "i", "c"))
            {
                this.ct = this.RemoveSuffix(this.ct, "i");
            }
        }

        /*
         * Residual suffix
       *
       * If the word ends with one of the suffixes (os a i o á í ó)
       * in RV, delete it
       *
        */
        private void Step4()
        {
            ////if (this.rv == null) return;

            ////if (this.Suffix(this.rv, "os"))
            ////{
            ////    this.ct = this.RemoveSuffix(this.ct, "os"); return;
            ////}
            ////if (this.Suffix(this.rv, "a"))
            ////{
            ////    this.ct = this.RemoveSuffix(this.ct, "a"); return;
            ////}
            ////if (this.Suffix(this.rv, "i"))
            ////{
            ////    this.ct = this.RemoveSuffix(this.ct, "i"); return;
            ////}
            ////if (this.Suffix(this.rv, "o"))
            ////{
            ////    this.ct = this.RemoveSuffix(this.ct, "o");
            ////}
        }

        /*
         * If the word ends with one of ( e é ê) in RV,delete it,
       * and if preceded by 'gu' (or 'ci') with the 'u' (or 'i') in RV,
       * delete the 'u' (or 'i')
       *
       * Or if the word ends ç remove the cedilha
       *
        */
        private void Step5()
        {
            ////if (this.rv == null) return;

            ////if (this.Suffix(this.rv, "e"))
            ////{
            ////    if (this.SuffixPreceded(this.rv, "e", "gu"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "e");
            ////        this.ct = this.RemoveSuffix(this.ct, "u");
            ////        return;
            ////    }

            ////    if (this.SuffixPreceded(this.rv, "e", "ci"))
            ////    {
            ////        this.ct = this.RemoveSuffix(this.ct, "e");
            ////        this.ct = this.RemoveSuffix(this.ct, "i");
            ////        return;
            ////    }

            ////    this.ct = this.RemoveSuffix(this.ct, "e");
            ////}
        }
    }
}