import requests

# Constants
DRAFT_ID = 2  # Replace with your actual draft ID
FRANCHISE_IDS = [2, 4, 5, 6, 7, 8]  # Replace with your actual franchise IDs
BASE_URL = "http://localhost:5062/api/draft"

def get_available_teams(draft_id):
    try:
        response = requests.get(f"{BASE_URL}/{draft_id}/available-teams")
        response.raise_for_status()
        
        # Extracting the list of available teams from the response
        data = response.json()
        available_teams = data.get('$values', [])
        
        return available_teams
    except requests.exceptions.RequestException as e:
        print(f"Error fetching available teams: {e}")
        return []

def make_pick(draft_id, franchise_id, team_id):
    try:
        payload = {
            "franchiseId": franchise_id,
            "teamId": team_id
        }
        response = requests.post(f"{BASE_URL}/{draft_id}/pick", json=payload)
        response.raise_for_status()
        print(f"Franchise {franchise_id} successfully picked team {team_id}.")
    except requests.exceptions.RequestException as e:
        print(f"Failed to pick team {team_id} for franchise {franchise_id}. Response: {e}")

def run_draft():
    for round_number in range(1, 6):  # 5 rounds
        print(f"Starting Round {round_number}")
        
        # Snake draft order: forward in odd rounds, reverse in even rounds
        if round_number % 2 == 1:
            order = FRANCHISE_IDS
        else:
            order = FRANCHISE_IDS[::-1]
        
        for franchise_id in order:
            available_teams = get_available_teams(DRAFT_ID)
            if not available_teams:
                print("No available teams found.")
                continue

            # Ensure that available_teams is a list and pick the first available team
            if isinstance(available_teams, list) and available_teams:
                team_id = available_teams[0]  # Pick the first available team
                make_pick(DRAFT_ID, franchise_id, team_id)
            else:
                print(f"Unexpected data format or empty list for available teams: {available_teams}")
        
        print(f"Round {round_number} completed")

    print("Draft completed")

if __name__ == "__main__":
    run_draft()
