import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class Meeting {
  final int id;
  final String meetingName;

  Meeting({required this.id, required this.meetingName});

  factory Meeting.fromJson(Map<String, dynamic> json) => Meeting.fromJson(json);
  Map<String, dynamic> toJson() => this.toJson();
}

class MeetingService {
  final String _baseUrl = 'https://localhost:7022/api/Meeting';

  Future<List<Meeting>> fetchMeetings() async {
    final response = await http.get(Uri.parse('$_baseUrl/meetings'));

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      return jsonResponse.map((meeting) => Meeting.fromJson(meeting)).toList();
    } else {
      throw Exception('Failed to load meetings');
    }
  }
}
