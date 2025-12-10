#include <string>
#include <vector>

using std::string, std::vector;

vector<string> splitStringByLines(const string& text);
vector<string> splitStringByDelimiter(const string& text, const char& delimiter);
std::pair<string, string> splitStringByFirstOccurence(const string& text, const string token);
std::pair<long, long> parseRange(const string& range);
