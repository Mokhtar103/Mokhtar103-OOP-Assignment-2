public class Solution {
    public int MaxVowels(string s, int k) {
        int vowels = 0;

        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
                vowels++;
        }

        int maxVowels = vowels;

        for (int right = k; right < s.Length; right++)
        {
            int left = right - k;

            if (IsVowel(s[left]))
                vowels--;

            if (IsVowel(s[right]))
                vowels++;

            maxVowels = Math.Max(maxVowels, vowels);
        }

        return maxVowels;
    }

    private bool IsVowel(char c)
    {
        return c == 'a' ||
               c == 'e' ||
               c == 'i' ||
               c == 'o' ||
               c == 'u';
    }
}