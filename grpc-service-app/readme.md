# Przykładowa Usługa gRPC
Uruchom ten projekt, a następnie projekt grpc-client-app, który wywołuje procedurę z serwisu.
# Zdanie 1
1. Przedefiniuj plik .proto, aby procedura `Convert` przyjmowała komunikat z trzema polami:
    - wartością np. value
    - jednostką przeliczanej wartości, value_unit
    - jednostką, do której wartość należy przeliczyć np. target_unit
2. Zmień ciało metody w klasie `ConvertServide`, aby przeliczała zgodnie z nowym komunikatem
3. Przenieś nowy plik .proto do projektu grpc-client.app
4. Zmień wywołanie metody klienta zgodnie ze zmianami
# Zadanie 2
1. Zdefiniuj nową wersję usługi, która posługuje się jednostkami w postaci typu enum zapisanym w pliku `protobuf`.
2. Zefiniuj nowy serwis usługi zgodnie z zasadami:
    - próba wykonania konwersji między niezgodnymi jednostami powinna zgłaszać własny wyjątek np. IncompatibileUnitConversion z komunikatem wyjaśniającym niemożnośc konwersji między np. kilogramem a metrem.
    - próba wykonania konwersji dla wartości niepoprawnej powinna zgłaszać wyjątek np. konwersja temperatury -500 C. 
    - typ wyliczeniowy jednostek powinien być wygenerowany na podstawie pliku `proto`.
