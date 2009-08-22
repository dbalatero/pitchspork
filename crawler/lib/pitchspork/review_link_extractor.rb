module Pitchspork
  class ReviewLinkExtractor
    def initialize(html)
      @doc = Nokogiri::HTML(html)
    end

    def reviews
      @reviews ||= begin
        links = @doc.css('div.review-list ul li a')
        reviews = {}
        links.each do |link|
          href = link.attributes['href'].to_s
          if href =~ /\/reviews\/albums\/\d+/
            href = add_domain_to_link(href)
            title = link.content
            reviews[title] = href
          end
        end
        reviews
      end
    end

    def next_page
      @next_page ||= begin
        link = @doc.css('p.text_right a.more-link').last
        if link and link.content =~ /Next/
          add_domain_to_link(link.attributes['href'])
        else
          nil
        end
      end
    end

    private
    def add_domain_to_link(href)
      unless href =~ /^https?:\/\//
        href = "http://pitchfork.com#{href}"
      end
      href
    end
  end
end
